using Microsoft.AspNetCore.SignalR;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Models.ChatHub.Request;
using TalkWave.Chat.Models.Messages.Request;

namespace TalkWave.Chat.Api.Hubs {
    
    public class ChatHub : Hub {
        
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMessageService messageService, IChatService chatService, ILogger<ChatHub> logger) {

            _chatService = chatService;
            _messageService = messageService;
            _logger = logger;

        }

        private string GetUserId() => Context.UserIdentifier!;

        public async Task JoinHub(Guid userId) {

            try {

                var userChats = await _chatService.GetChatsIdsForUserAsync(userId);

                foreach (var chatId in userChats) {

                    await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

                }

                _logger.LogInformation($"User {userId} connected to hub");

            } catch(Exception ex) {

                _logger.LogError(ex, $"Error when {userId} connecting to hub");

                throw new Exception(ex.Message);

            }

        }

        public async Task SendMessage(CreateMessageRequestModel messageModel) {

            try {

                if (messageModel == null)
                    throw new ArgumentNullException(nameof(messageModel));

                if (string.IsNullOrWhiteSpace(messageModel.Content))
                    throw new ArgumentException("Message content cannot be empty");

                var messageResponse = await _messageService.CreateMessageAsync(messageModel);

                await Clients.Group(messageModel.ChatId.ToString()).SendAsync("ReceiveMessage", messageResponse);

                _logger.LogInformation($"Message {messageResponse.Id} sent to chat {messageModel.ChatId} by {messageModel.SenderId}");

            } catch(Exception ex) {

                _logger.LogError(ex, $"Error sending message to chat {messageModel?.ChatId} by user {messageModel?.SenderId}");

                throw new HubException($"Failed to send message: {ex.Message}", ex);

            }

        }




    }

}
