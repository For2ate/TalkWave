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

        public async Task<IEnumerable<ChatEntity>> GetCommonChatsForUsersAsync(Guid userId1, Guid userId2) {

            try {

                return await _dbSet
                    .Where(cm => cm.UserId == userId1)
                    .Join(
                        _dbSet.Where(cm => cm.UserId == userId2),
                        cm1 => cm1.ChatId,
                        cm2 => cm2.ChatId,
                        (cm1, cm2) => cm1.Chat)
                    .Where(c => !c.IsGroupChat)
                    .ToListAsync();

            } catch {

                throw;

            }

        }

    }

}
