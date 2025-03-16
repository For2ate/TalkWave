using TalkWave.User.Models.UserModels.Response;

namespace TalkWave.User.Api.Core.Interfaces {

    public interface IUserService {

        Task<UserFullResponseModel> GetUserByLoginAsync(string login);

        Task<UserFullResponseModel> GetUserByEmailAsync(string email);

        Task<IEnumerable<UserFullResponseModel>> GetAllUsersAsync();

    }

}
