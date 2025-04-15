namespace TalkWave.Chat.Models.Chats.Request {

    public class CreatePersonalChatModel {
        
        public Guid SenderUserId { get; set; }

        public Guid RecipientUserId { get; set; }

        public string Message { get; set; }

    }

}
