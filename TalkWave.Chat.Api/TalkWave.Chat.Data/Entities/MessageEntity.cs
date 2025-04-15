using TalkWave.Common.Message;

namespace TalkWave.Chat.Data.Entities {
    
    public class MessageEntity : BaseEntity {

        public string Content { get; set; }

        public MessageStatus Status { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Guid SenderId { get; set; }
        public virtual UserEntity Sender { get; set; }

        public Guid ChatId { get; set; }
        public virtual ChatEntity Chat { get; set; }


    }

}
