using TalkWave.Common.Message;

namespace TalkWave.Chat.Models.Messages.Request {

    public class CreateMessageRequestModel {

        public Guid SenderId { get; set; }

        public Guid ChatId { get; set; }

        public string Content { get; set; }

    }

}
