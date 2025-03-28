using TalkWave.Common.Message;

namespace TalkWave.Chat.Models.Messages.Response {

    public class MessageFullResponseModel {

        public Guid SenderId { get; set; }

        public Guid ChatId { get; set; }

        public string Content { get; set; }

        public MessageStatus Status { get; set; }

        public DateTime SentAt { get; set; }

    }

}
