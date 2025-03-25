using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {

    public interface IChatMembersRepository {

        Task<IEnumerable<ChatMemberEntity>> GetAllChatsForUserAsync(Guid UserId);

    }

}
