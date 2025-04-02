namespace TalkWave.Chat.Models.Messages.Request {

    public class GetNMessagesRequestModel {

        public Guid ChatId { get; set; }

        public Guid MessageId { get; set; }

        public int Take { get; set; }

    }
        
}
