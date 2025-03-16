using TalkWave.User.Models.UserModels.Request;
using TalkWave.User.Models.UserModels.Response;

namespace TalkWave.User.Api.Core.Interfaces {

    public interface IAuthService {

        Task<UserFullResponseModel> RegisterAsync(UserRegisterRequestModel model);

        Task<UserFullResponseModel> LoginAsync(UserLoginRequestModel model);

    }

}
