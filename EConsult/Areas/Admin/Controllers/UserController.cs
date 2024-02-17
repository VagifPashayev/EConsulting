using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EConsult.Areas.Admin.ViewModels.User;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Database.Models;
using EConsult.Services.Concretes;

namespace EConsult.Areas.Admin.Controllers;


[Route("admin/users")]
[Area("admin")]
[Authorize(Roles = Role.Names.SuperAdmin)]
public class UserController : Controller
{ 
    private readonly EConsultDbContext _context; // Replace with your DbContext
    private readonly OnlineUserTracker _onlineUserTracker;

    public UserController(EConsultDbContext context, OnlineUserTracker onlineUserTracker)
    {
        _context = context;
        _onlineUserTracker = onlineUserTracker;
    }

    // GET: api/User
    [HttpGet]
    public IActionResult Index()
    {
        var users = _context.Users
            .Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                IsConfirmed = u.IsConfirmed,
                IsOnline = _onlineUserTracker.DoesUserHaveConnectionId(u), //Your task,
                Role = u.Role.ToString(),
            })
            .ToList(); // Replace with your user entity DbSet


        return View(users);
    }
}
