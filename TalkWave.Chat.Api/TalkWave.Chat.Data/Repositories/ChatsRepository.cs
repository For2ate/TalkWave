using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;

namespace TalkWave.Chat.Data.Repositories {

    public class ChatsRepository : BaseRepository<ChatEntity>, IChatsRepository {

        public ChatsRepository(ChatsContext context) : base(context) { }

  


    }

}
