using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;

namespace TalkWave.Chat.Data.Repositories {

    public class ChatMembersRepository : BaseRepository<ChatMemberEntity>, IChatMembersRepository {

        public ChatMembersRepository(ChatsContext context) : base(context) { }

        public async Task<IEnumerable<ChatMemberEntity>> GetAllChatsForUserAsync(Guid id) {

            try {

                var chats = await _dbSet.Where(cm => cm.UserId == id).ToListAsync();

                return chats;

            } catch {

                throw;

            }

        }

    }

}
