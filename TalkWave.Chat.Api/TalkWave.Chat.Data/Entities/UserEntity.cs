namespace TalkWave.Chat.Data.Entities {
    
    public class UserEntity : BaseEntity {

        public virtual ICollection<MessageEntity> Messages { get; set; }

        public virtual ICollection<ChatMemberEntity>  ChatMembers { get; set; }

    }

}
