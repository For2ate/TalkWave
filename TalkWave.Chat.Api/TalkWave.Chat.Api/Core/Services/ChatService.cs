using AutoMapper;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Models.Chats.Response;

namespace TalkWave.Chat.Api.Core.Services {

    public class ChatService : IChatService {

        private readonly IChatsRepository chatsRepository;
        private readonly IBaseRepository<UserEntity> usersRepository;
        private readonly IMessagesRepository messagesRepository;
        private readonly IChatMembersRepository chatMembersRepository;

        private readonly IMapper chatMapper;

        public ChatService(IChatsRepository chatsRepository, IBaseRepository<UserEntity> usersRepository,
            IMessagesRepository messagesRepository, IChatMembersRepository membersChatRepository,
            IMapper chatMapper
            ) {
            this.chatsRepository = chatsRepository;
            this.usersRepository = usersRepository;
            this.messagesRepository = messagesRepository;
            this.chatMembersRepository = membersChatRepository;
            this.chatMapper = chatMapper;
        }

        public async Task<IEnumerable<ChatFullResponseModel>> GetChatsForUserAsync(Guid userId) {

            try {

                var chatsId = await chatMembersRepository.GetAllChatsForUserAsync(userId);

                var chats = new List<ChatFullResponseModel>();

                foreach (var chatMember in chatsId) {

                    var chatEntity = await chatsRepository.GetByIdAsync(chatMember.ChatId);

                    var chat = chatMapper.Map<ChatFullResponseModel>(chatEntity);

                    chat.Role = chatMember.Role;

                    chats.Add(chat);

                }

                return chats;


            } catch (Exception ex) {

                Console.WriteLine("Service error\n\n\n" + ex.Message);

                throw;

            }

        }

    }

}
