using TalkWave.User.Data.Entities;

namespace TalkWave.User.Data.Interfaces {

    public interface IUserRepository : IBaseRepository<UserEntity> {

        Task<UserEntity> GetByLoginAsync(string login);

        Task<UserEntity> GetByEmailAsync(string login);

    }

}
