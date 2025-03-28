using TalkWave.Chat.Models.Messages.Request;
using TalkWave.Chat.Models.Messages.Response;

namespace TalkWave.Chat.Api.Core.Interfaces {
    
    public interface IMessageService {

        Task<MessageFullResponseModel> CreateMessageAsync(CreateMessageRequestModel model);

        Task<MessageFullResponseModel> UpdateMessageAsync(UpdateMessageRequestModel model);

    }

}
