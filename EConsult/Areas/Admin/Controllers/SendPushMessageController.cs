using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EConsult.Areas.Admin.ViewModels.Notification;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Services.Abstracts;

namespace EConsult.Areas.Admin.Controllers
{
    [Route("admin/notification")] // Update route
    [Area("admin")]
    [Authorize(Roles = Role.Names.SuperAdmin)]
    public class SendPushMessageController : Controller
    {
        private readonly EConsultDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public SendPushMessageController(EConsultDbContext dbContext, IUserService userService, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpGet("SendNotification")]
        public IActionResult SendNotification()
        {
            UserNotificationViewModel notificationViewModel = new()
            {
                Users = _dbContext.Users.ToList()
            };

            return View(notificationViewModel);
        }

        [HttpPost("SendNotification")]
        public IActionResult SendNotification(UserNotificationViewModel userNotificationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userNotificationViewModel);
            }

            foreach (var userId in userNotificationViewModel.SelectedUsersId)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
                
                if (user is null)
                {
                    return NotFound();
                }

                _notificationService.SendPushNotification(user, userNotificationViewModel.Title, userNotificationViewModel.Content);
            }

            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
