using AutoMapper;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Data.MappingProfiles {

    public class ChatMappingProfile : Profile {

        public ChatMappingProfile() {

            CreateMap<ChatEntity, ChatFullResponseModel>();

           

        }

    }

}
