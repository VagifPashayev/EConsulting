using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using EConsult.Services.Abstracts;
using EConsult.Services.Concretes;

namespace EConsult.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatHub(
            ILogger<ChatHub> logger,
            IChatService chatService,
            IUserService userService)
        {
            _logger = logger;
            _chatService = chatService;
            _userService = userService;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"New connection established : {Context.ConnectionId}");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("user-disconnected", _chatService.GetConnectionIds(Context.ConnectionId));
            _chatService.RemoveConnectionId(_chatService.GetConnectionIds(Context.ConnectionId), Context.ConnectionId);

            _logger.LogInformation($"Connection finished established : {Context.ConnectionId}");

            return base.OnDisconnectedAsync(exception);
        }
        public async Task JoinRoom(string roomId, string userId)
        {
            _chatService.AddConnectionId(userId, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("user-connected", userId);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", _userService.GetCurrentUserFullName(), message);;
        }
    }
}
