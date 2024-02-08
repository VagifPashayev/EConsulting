using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Services.Abstracts;
using EConsult.Services.Concretes;
using EConsult.ViewModels;

namespace EConsult.Controllers;

[Authorize]
[Route("account")]
public class AccountController : Controller
{
    private readonly EConsultDbContext _dbContext;
    private readonly IUserService _userService;

    public AccountController(EConsultDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet("orders")]
    public IActionResult Orders()
    {
        var orders = _dbContext.Orders
            .Where(o => o.UserId == _userService.CurrentUser.Id)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                LobbyCode = o.LobbyCode,
            })
            .ToList();

        return View(orders);
    }


    [HttpGet("addresses")]
    public IActionResult Addresses()
    {
        return View();
    }

    [HttpGet("notifications")]
    public IActionResult Notifications()
    {
        var notifications = _dbContext.AlertMessages
            .Where(x => x.User == _userService.CurrentUser).ToList();
        return View(notifications);
    }

    [HttpGet("account-details")]
    public IActionResult AccountDetails()
    {
        return View();
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        //logic

        return RedirectToAction("index", "home");
    }

}
