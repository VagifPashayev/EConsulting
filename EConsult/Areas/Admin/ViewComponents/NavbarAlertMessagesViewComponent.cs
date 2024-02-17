using Microsoft.AspNetCore.Mvc;
using EConsult.Areas.Admin.ViewModels.AlertMessage;
using EConsult.Database;
using EConsult.Services.Abstracts;
using EConsult.ViewModels;

namespace EConsult.Areas.Admin.ViewComponents;

public class NavbarAlertMessagesViewComponent : ViewComponent
{
    private readonly EConsultDbContext _pustokDbContext;
    private readonly IUserService _userService;

    public NavbarAlertMessagesViewComponent(
        EConsultDbContext pustokDbContext,
        IUserService userService)
    {
        _pustokDbContext = pustokDbContext;
        _userService = userService;
    }

    public IViewComponentResult Invoke()
    {
        var alertMessages = _pustokDbContext.AlertMessages
            .Where(am => am.UserId == _userService.CurrentUser.Id)
            .OrderByDescending(o => o.CreatedAt)
            .Select(am => new AlertMessageViewModel
            {
                Id = am.Id,
                Title = am.Title,
                Content = am.Content,
                CreatedAt = am.CreatedAt
            })
            .ToList();
        
        return View(alertMessages);
    }
}
