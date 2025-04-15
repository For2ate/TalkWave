namespace TalkWave.Chat.Models.ChatHub.Request {

    public class JoinChatRequestModel {
 
        public Guid UserId { get; set; }

        public Guid ChatId { get; set; }

        public Guid LastMessageId { get; set; }
    
    }

}
