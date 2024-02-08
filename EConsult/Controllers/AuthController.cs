using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Database.Models;
using EConsult.Services.Abstracts;
using EConsult.Services.Concretes;
using EConsult.ViewModels;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace EConsult.Controllers;


[Route("auth")]
public class AuthController : Controller
{
    private readonly EConsultDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly IUserActivationService _userActivationService;


    public AuthController(
        EConsultDbContext dbContext,
        IUserService userService,
        IEmailService emailService,
        IUserActivationService userActivationService)
    {
        _dbContext = dbContext;
        _userService = userService;
        _emailService = emailService;
        _userActivationService = userActivationService;
    }

    #region Login

    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        if (_userService.IsCurrentUserAuthenticated())
        {
            return RedirectToAction("index", "home");
        }


        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _dbContext.Users.SingleOrDefault(u => u.Email == model.Email);
        if (user is null)
        {
            ModelState.AddModelError("Password", "Email not found");
            return View(model);
        }

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            ModelState.AddModelError("Password", "Password is not valid");
            return View(model);
        }

        if (!user.IsConfirmed)
        {
            ModelState.AddModelError(string.Empty, "Account is not confirmed");
            return View(model);
        }



        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
        };

        claims.AddRange(_userService.GetClaimsAccordingToRole(user));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

        return RedirectToAction("index", "home");
    }

    #endregion

    #region Register

    [HttpGet("register")]
    public IActionResult Register()
    {
        if (_userService.IsCurrentUserAuthenticated())
        {
            return RedirectToAction("index", "home");
        }

        return View();
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterViewModel model, [FromServices] IUserActivationService userActivationService)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (_dbContext.Users.Any(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "This email already used");
            return View(model);
        }

        var user = new User
        {
            Name = model.Name,
            LastName = model.LastName,
            Email = model.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        };

        _dbContext.Users.Add(user);
        _userActivationService.CreateAndSendActivationToken(user);

        _dbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("verify-account/{token}", Name = "register-account-verification")]
    public IActionResult VerifyAccount(Guid token)
    {
      

        var activation = _dbContext.UserActivations
            .Include(ua => ua.User)
            .SingleOrDefault(ua => 
                !ua.User.IsConfirmed &&
                ua.Token == token && 
                ua.ExpireDate > DateTime.UtcNow);

        if (activation == null)
            return BadRequest("Token not found or already expire");
     
        activation.User.IsConfirmed = true;
        _dbContext.SaveChanges();

        return RedirectToAction("login", "auth");
    }


    #endregion

    #region Logout

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("index", "home");
    }


    #endregion

    public IActionResult Notifications()
    {
        var notifications = _dbContext.AlertMessages
            .Include(x => x.User)
            .Where(x => x.User == _userService.CurrentUser).ToList();
        
        return View(notifications);
    }
}
