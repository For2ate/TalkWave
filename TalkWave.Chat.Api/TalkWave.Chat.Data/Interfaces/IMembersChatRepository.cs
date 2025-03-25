using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {

    public interface IMembersChatRepository {

        Task<IEnumerable<ChatMemberEntity>> GetAllChatsForUserAsync(Guid UserId);

        Task ChangeRoleUserAsync(Guid UserId, Guid ChatId);

    }

}
