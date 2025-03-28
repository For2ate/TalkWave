using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;

namespace TalkWave.Chat.Data.Repositories {

    public class ChatsRepository : BaseRepository<ChatEntity>, IChatsRepository {

        public ChatsRepository(ChatsContext context) : base(context) { }

        public async Task<ChatEntity?> GetChatWithMembersAsync(Guid chatId) {
            return await _dbSet
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }


    }

}
