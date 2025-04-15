using AutoMapper;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Models.ChatMembers;
using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Data.MappingProfiles {

    public class ChatMappingProfile : Profile {

        public ChatMappingProfile() {

            CreateMap<ChatEntity, ChatFullResponseModel>()
                .ForMember(dest => dest.ChatMembers,
                    opt => opt.MapFrom(src => src.Members.Select(m => new ChatMemberModel {
                        Id = m.UserId,
                        Role = m.Role
                    })));




        }

    }

}
