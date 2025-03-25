using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;

namespace TalkWave.Chat.Data.Repositories {

    public class MessagesRepository: BaseRepository<MessageEntity>, IMessagesRepository {

        public MessagesRepository(ChatsContext context) : base(context) { }

        public async Task<IEnumerable<MessageEntity>> GetNMessagesByChatIdAsync(Guid chatId, int take) {

            try {

                return await _dbSet.Where(m => m.ChatId == chatId)
                    .OrderByDescending(m => m.SentAt)
                    .Take(take)
                    .AsNoTracking()
                    .ToListAsync();

            } catch (Exception ex) {

                Console.WriteLine("Repository error./n/n/n" + ex.Message);

                throw;

            }

        }

    }

}
