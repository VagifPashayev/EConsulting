using Microsoft.AspNetCore.SignalR;
using EConsult.Areas.Admin.ViewModels.AlertMessage;
using EConsult.Contracts;
using EConsult.Database;
using EConsult.Database.Models;
using EConsult.Exceptions;
using EConsult.Hubs;
using EConsult.Services.Abstracts;
using System.Text;

namespace EConsult.Services.Concretes;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly EConsultDbContext _pustokDbContext;
    private readonly IUserService _userService;
    private readonly IHubContext<AlertMessageHub> _hubContext;
    private readonly IAlertMessageService _aletMessageService;

    public NotificationService(
        IEmailService emailService,
        EConsultDbContext pustokDbContext,
        IUserService userService,
        IHubContext<AlertMessageHub> hubContext,
        IAlertMessageService aletMessageService)
    {
        _emailService = emailService;
        _pustokDbContext = pustokDbContext;
        _userService = userService;
        _hubContext = hubContext;
        _aletMessageService = aletMessageService;
    }

    public void SendOrderEmailNotification(Order order)
    {
        _emailService.SendEmail(EmailTemplates.Order.SUBJECT, $"Your lobby code: {order.LobbyCode}", order.User.Email);
    }

    public void SendPushNotification(User user, string title, string content)
    {
        var alertMessage = new AlertMessage
        {
            Title = title,
            Content = content,
            UserId = user.Id
        };

        _pustokDbContext.AlertMessages.Add(alertMessage);

        var connectIds = _aletMessageService.GetConnectionIds(user);

        var alerMessageViewModel = new AlertMessageViewModel
        {
            Title = alertMessage.Title,
            Content = alertMessage.Content,
            CreatedAt = DateTime.Now
        };

        _hubContext.Clients
            .Clients(connectIds)
            .SendAsync("ReceiveAlertMessage", alerMessageViewModel)
            .Wait();
    }
}
