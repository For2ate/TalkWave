using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.Reflection;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Models.Chats.Request;
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

                    var chatEntity = await chatsRepository.GetChatWithMembersAsync(chatMember.ChatId);

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

        public async Task<ChatFullResponseModel> CreatePersonalChatAsync(CreatePersonalChatModel createModel) {
 
            using var transaction = await chatsRepository.BeginTransactionAsync();

            try {

                var senderUser = await GetOrCreateUserAsync(createModel.SenderUserId);
                var recipientUser = await GetOrCreateUserAsync(createModel.RecipientUserId);

                var existingChat = await FindExistingPersonalChatAsync(createModel.SenderUserId, createModel.RecipientUserId);
                
                if (existingChat != null) {
                
                    await transaction.CommitAsync();

                    var chatex = chatMapper.Map<ChatFullResponseModel>(existingChat);

                    chatex.Role = Common.ChatMember.ChatMemberRole.Owner;

                    return chatex;
                
                }

                var newChat = new ChatEntity {
                    Id = Guid.NewGuid(),
                    Name = "",
                    IsGroupChat = false,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = createModel.SenderUserId,
                    Members = new List<ChatMemberEntity>(),
                    LastMessageId = null,
                };

                var sendeMember = new ChatMemberEntity {
                    ChatId = newChat.Id,
                    Chat = newChat,
                    UserId = senderUser.Id,
                    User = senderUser,
                    Role = Common.ChatMember.ChatMemberRole.Owner
                };
                
                var recipientMember = new ChatMemberEntity {
                    ChatId = newChat.Id,
                    Chat = newChat,
                    UserId = recipientUser.Id,
                    User = recipientUser,
                    Role = Common.ChatMember.ChatMemberRole.Owner
                };

                newChat.Members.Add(sendeMember);
                newChat.Members.Add(recipientMember);

                await chatsRepository.AddAsync(newChat);

                if (!string.IsNullOrWhiteSpace(createModel.Message)) {

                    var initialMessage = new MessageEntity {
                        Content = createModel.Message,
                        ChatId = newChat.Id,
                        SenderId = sendeMember.Id,
                        Chat = newChat,
                        Sender = senderUser,
                        Status = Common.Message.MessageStatus.Sent,
                        SentAt = DateTime.UtcNow,
                    };

                    await messagesRepository.AddAsync(initialMessage);

                    newChat.LastMessageId = initialMessage.Id;
                    await chatsRepository.UpdateAsync(newChat);

                }


                await transaction.CommitAsync();

                var chat = chatMapper.Map<ChatFullResponseModel>(newChat);
                chat.Role = Common.ChatMember.ChatMemberRole.Owner;

                return chat;

            } catch (Exception ex) {

                await transaction.RollbackAsync();

                Console.WriteLine("Service error\n\n\n" + ex.Message);

                throw;

            }

        }

        private async Task<UserEntity> GetOrCreateUserAsync(Guid userId) {

            try {

                var user = await usersRepository.GetByIdAsync(userId);

                if (user == null) {

                    user = new UserEntity {

                        Id = userId

                    };

                    await usersRepository.AddAsync(user);

                }

                return user;

            } catch (Exception ex) {

                throw;


            }

        }

        private async Task<ChatEntity?> FindExistingPersonalChatAsync(Guid userId1, Guid userId2) {

            try {

                var commonChats = await chatMembersRepository.GetCommonChatsForUsersAsync(userId1, userId2);

                var commonChat = commonChats.FirstOrDefault(c => !c.IsGroupChat);

                if (commonChat != null) {

                    commonChat = await chatsRepository.GetChatWithMembersAsync(commonChat.Id);

                }

                return commonChat;
            
            } catch(Exception ex) {

                Console.WriteLine(ex.Message);
                return null;


            }

        }



    }

}
