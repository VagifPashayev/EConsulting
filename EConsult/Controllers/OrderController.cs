using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Database.Models;
using EConsult.Services.Abstracts;

namespace EConsult.Controllers;

[Route("order")]
[Authorize]
public class OrderController : Controller
{
    private readonly INotificationService _nofitificationService;
    private readonly EConsultDbContext _pustokDbContext;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly IFileService _fileService;

    public OrderController(EConsultDbContext pustokDbContext, IUserService userService, IOrderService orderService, IFileService fileService, INotificationService nofitificationService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
        _orderService = orderService;
        _fileService = fileService;
        _nofitificationService = nofitificationService;
    }

    [HttpGet("place-order/{id}")]
    public IActionResult PlaceOrder(int id)
    {
        var productExists = _pustokDbContext.Products.Any(p => p.Id == id);
        if (!productExists) { return BadRequest(); }

        var order = new Order
        {
            ProductId = 1,
            LobbyCode = Guid.NewGuid().ToString(),
            User = _userService.CurrentUser
        };
        

        _nofitificationService.SendOrderEmailNotification(order);

        _pustokDbContext.Orders.Add(order);
        _pustokDbContext.SaveChanges();

        return RedirectToAction("orders", "account");
    }
}
