using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {
    
    public interface IMessagesRepository : IBaseRepository<MessageEntity>{

        Task<IEnumerable<MessageEntity>> GetNMessagesByChatIdAsync(Guid chatId, Guid fromMessageId, int take);

    }

}
