using AutoMapper;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Models.Messages.Request;
using TalkWave.Chat.Models.Messages.Response;

namespace TalkWave.Chat.Data.MappingProfiles {

    public class MessageMappingProfile : Profile {
    
        public MessageMappingProfile() {

            CreateMap<MessageEntity,MessageFullResponseModel>();

            CreateMap<CreateMessageRequestModel, MessageEntity>()
                .AfterMap((srs, dest) => {

                    dest.Id = Guid.NewGuid();
                    dest.SentAt = DateTime.UtcNow;

                });

        }
    
    }

}
