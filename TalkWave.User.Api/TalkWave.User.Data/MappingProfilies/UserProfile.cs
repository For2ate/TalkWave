using AutoMapper;
using TalkWave.User.Data.Entities;
using TalkWave.User.Models.UserModels.Response;
using TalkWave.User.Models.UserModels.Request;

namespace TalkWave.User.Data.MappingProfilies {

    public class UserProfile : Profile {

        public UserProfile() {

            CreateMap<UserEntity, UserFullResponseModel>();


            CreateMap<UserRegisterRequestModel, UserEntity>();

        }

    }

}
