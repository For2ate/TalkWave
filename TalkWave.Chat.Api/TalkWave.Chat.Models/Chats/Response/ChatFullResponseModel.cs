using TalkWave.Common.ChatMember;

namespace TalkWave.Chat.Models.Chats.Response {

    public class ChatFullResponseModel {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsGroupChat { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public ChatMemberRole Role { get; set; }

        public Guid LastMassege { get; set; }

    }

}
