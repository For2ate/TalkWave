using Microsoft.AspNetCore.SignalR;
using TalkWave.Chat.Api.Core.Interfaces;

namespace TalkWave.Chat.Api.Hubs {
    
    public class ChatHub : Hub {

        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService) {
            _messageService = messageService;
        }


    }

}
