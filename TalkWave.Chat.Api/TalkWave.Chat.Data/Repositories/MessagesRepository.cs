using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;

namespace TalkWave.Chat.Data.Repositories {

    public class MessagesRepository: BaseRepository<MessageEntity>, IMessagesRepository {

        public MessagesRepository(ChatsContext context) : base(context) { }

        public async Task<IEnumerable<MessageEntity>> GetNMessagesFromMessageAsync(Guid chatId, Guid fromMessageId, int take) {

            try {

                var fromMessage = await _dbSet
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == fromMessageId);

                if (fromMessage == null)
                    return Enumerable.Empty<MessageEntity>();

                return await _dbSet
                    .Where(m => m.ChatId == chatId &&
                               m.SentAt <= fromMessage.SentAt)
                    .OrderByDescending(m => m.SentAt)
                    .Take(take)
                    .Reverse()
                    .AsNoTracking()
                    .ToListAsync();

            } catch (Exception ex) {

                Console.WriteLine("Repository error./n/n/n" + ex.Message);

                throw;

            }

        }

    }

}
