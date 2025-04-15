using AutoMapper;
using AutoMapper.Configuration.Annotations;
using TalkWave.User.Api.Core.Interfaces;
using TalkWave.User.Data.Interfaces;
using TalkWave.User.Models.UserModels.Response;


namespace TalkWave.User.Api.Core.Services {
    
    public class UserService : IUserService {

        private readonly IUserRepository _useRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository useRepository, IMapper mapper) {

            _useRepository = useRepository;
            _mapper = mapper;

        }

        public async Task<UserFullResponseModel> GetUserByIdAsync(Guid id) {

            try {

                var user = await _useRepository.GetByIdAsync(id);

                return _mapper.Map<UserFullResponseModel>(user);

            } catch (Exception ex) {

                throw;

            }

        }

        public async Task<UserFullResponseModel> GetUserByLoginAsync(string login) {

            try {

                var user = await _useRepository.GetByLoginAsync(login);

                return _mapper.Map<UserFullResponseModel>(user);

            } catch (Exception ex) {

                throw;

            }

        }

        public async Task<UserFullResponseModel> GetUserByEmailAsync(string email) {

            try {

                var user = await _useRepository.GetByEmailAsync(email);

                return _mapper.Map<UserFullResponseModel>(user);

            } catch (Exception ex) {

                throw;

            }

        }

        public async Task<IEnumerable<UserFullResponseModel>> GetAllUsersAsync() {

            try {

                var users = await _useRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<UserFullResponseModel>>(users);

            } catch(Exception ex) {

                throw;

            }

        }

    }

}
