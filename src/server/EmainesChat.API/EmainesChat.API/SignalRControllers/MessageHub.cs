using EmainesChat.API.Controllers;
using EmainesChat.Business.Commands;
using EmainesChat.Business.Messages;
using EmainesChat.Business.Users;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;

namespace EmainesChat.API.SignalRControllers
{
    public class MessageHub : Hub
    {
        private readonly MessageService _messageService;

        public MessageHub(MessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task CreateMessage(MessageCreateCommand command)
        {
            var message = await _messageService.Create(command);
            await Clients.All.SendAsync("MessageCreated", message);
        }
    }
}
