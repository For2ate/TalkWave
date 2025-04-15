namespace TalkWave.Chat.Data.Entities {
    
    public class ChatEntity : BaseEntity {

        public string Name { get; set; }

        public bool IsGroupChat { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }
        
        public Guid? LastMessageId { get; set; }
        public virtual MessageEntity LastMessage { get; set; }

        public virtual ICollection<MessageEntity> Messages { get; set; }
        public virtual ICollection<ChatMemberEntity> Members { get; set; }


    }

}
