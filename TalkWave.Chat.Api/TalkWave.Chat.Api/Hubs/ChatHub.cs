using Microsoft.AspNetCore.SignalR;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Models.ChatHub.Request;

namespace TalkWave.Chat.Api.Hubs {
    
    public class ChatHub : Hub {
        
        private readonly IMessageService _messageService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMessageService messageService, ILogger<ChatHub> logger) {

            _messageService = messageService;
            _logger = logger;

        }

        private string GetUserId() => Context.UserIdentifier!;

        public async Task JoinChat(JoinChatRequestModel joinModel) {

            try {

                var messagesRequestModel = new Models.Messages.Request.GetNMessagesRequestModel {
                    ChatId = joinModel.ChatId,
                    MessageId = joinModel.LastMessageId,
                    Take = 50
                };

                var messages = await _messageService.GetNMessagesFromMessageAsync(messagesRequestModel);

                await Clients.Caller.SendAsync("ReceiveMessages", messages);

                await Groups.AddToGroupAsync(Context.ConnectionId, joinModel.ChatId.ToString());

                _logger.LogInformation($"User {joinModel.UserId} joined chat {joinModel.ChatId}");


            } catch (Exception ex) {

                _logger.LogError(ex, $"Error joining chat {joinModel?.ChatId} by user {joinModel?.UserId}");

                throw new HubException($"Join chat failed: {ex.Message}", ex);

            }

        }

    }

}
