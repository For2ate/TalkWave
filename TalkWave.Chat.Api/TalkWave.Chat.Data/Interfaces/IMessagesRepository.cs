using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {
    
    public interface IMessagesRepository {

        Task<IEnumerable<MessageEntity>> GetNMessagesByChatIdAsync(Guid chatId, int take);

    }

}
