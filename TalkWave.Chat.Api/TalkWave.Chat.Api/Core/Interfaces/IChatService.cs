using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Api.Core.Interfaces {

    public interface IChatService {

        Task<IEnumerable<ChatFullResponseModel>> GetChatsForUserAsync(Guid id);

    }

}
