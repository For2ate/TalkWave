using AutoMapper;
using System.Text.RegularExpressions;
using TalkWave.User.Api.Core.Interfaces;      
using TalkWave.User.Api.Core.Methods;
using TalkWave.User.Data.Entities;
using TalkWave.User.Data.Interfaces;
using TalkWave.User.Models.UserModels.Request;
using TalkWave.User.Models.UserModels.Response;


namespace TalkWave.User.Api.Core.Services {
    
    public class AuthService : IAuthService{

        private readonly IUserRepository _useRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository useRepository, IMapper mapper) {

            _useRepository = useRepository;
            _mapper = mapper;

        }

        public async Task<UserFullResponseModel> RegisterAsync(UserRegisterRequestModel model) {

            try {

                // Check if the login is already taken
                var existingUserByLogin = await _useRepository.GetByLoginAsync(model.Login);
                if (existingUserByLogin != null) {
                    throw new Exception("Login is already taken.");
                }

                var existingUserByEmail = await _useRepository.GetByEmailAsync(model.Email);   
                if (existingUserByEmail != null) {
                    throw new Exception("Email is already taken.");
                }

                var user = _mapper.Map<UserEntity>(model);

                user.Password = Hasher.HashPassword(model.Password);

                await _useRepository.AddAsync(user);

                return _mapper.Map<UserFullResponseModel>(user);

                throw new Exception("User registration failed.");

            } catch {

                throw;

            }

        }

        public async Task<UserFullResponseModel> LoginAsync(UserLoginRequestModel model) {

            try {

                // Регулярное выражение для проверки email
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                bool isEmail = Regex.IsMatch(model.Login, emailPattern);

                UserEntity user = null;

                if (isEmail) {

                    user = await _useRepository.GetByEmailAsync(model.Login);

                } else {

                    user = await _useRepository.GetByLoginAsync(model.Login);

                }

                if (user == null) {
                    throw new Exception("User not exist.");
                }

                if (Hasher.HashPassword(model.Password) != user.Password) {
                    throw new Exception("Incorect password.");
                }

                return _mapper.Map<UserFullResponseModel>(user);

            } catch(Exception ex) {

                throw;

            }

        }

    }

}
