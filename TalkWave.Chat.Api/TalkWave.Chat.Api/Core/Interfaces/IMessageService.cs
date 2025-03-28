using TalkWave.Chat.Models.Messages.Request;
using TalkWave.Chat.Models.Messages.Response;

namespace TalkWave.Chat.Api.Core.Interfaces {
    
    public interface IMessageService {

        Task<MessageFullResponseModel> GetMessageById(Guid id);

        Task<IEnumerable<MessageFullResponseModel>> GetNMessagesFromMessageAsync(GetNMessagesRequestModel model);

        Task<MessageFullResponseModel> CreateMessageAsync(CreateMessageRequestModel model);

        Task<MessageFullResponseModel> UpdateMessageAsync(UpdateMessageRequestModel model);

        Task DeleteMessageByIdAsync(Guid id);

    }

}
