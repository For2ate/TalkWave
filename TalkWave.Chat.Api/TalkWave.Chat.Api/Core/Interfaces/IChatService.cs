using TalkWave.Chat.Models.Chats.Request;
using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Api.Core.Interfaces {

    public interface IChatService {

        Task<IEnumerable<ChatFullResponseModel>> GetChatsForUserAsync(Guid id);

        Task<IEnumerable<Guid>> GetChatsIdsForUserAsync(Guid userId);

        Task<ChatFullResponseModel> CreatePersonalChatAsync(CreatePersonalChatModel model);

    }

}
