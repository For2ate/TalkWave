using TalkWave.Common.ChatMember;

namespace TalkWave.Chat.Data.Entities {
    
    public class ChatMemberEntity : BaseEntity {

        public Guid UserId { get; set; }
        public virtual  UserEntity User { get; set; }

        public Guid ChatId { get; set; }
        public virtual ChatEntity Chat { get; set; }

        public ChatMemberRole Role { get; set; }

    }

}
