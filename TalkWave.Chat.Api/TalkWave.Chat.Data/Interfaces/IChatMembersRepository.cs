using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {

    public interface IChatMembersRepository : IBaseRepository<ChatMemberEntity> {

        Task<IEnumerable<ChatMemberEntity>> GetAllChatsForUserAsync(Guid UserId);

        Task<IEnumerable<ChatEntity>> GetCommonChatsForUsersAsync(Guid userId1, Guid userId2);

    }

}
