using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {
    
    public interface IMessagesRepository {

        Task<IEnumerable<MessageEntity>> GetNMessagesByChatIdAsync(Guid ChatId, int take);

    }

}
