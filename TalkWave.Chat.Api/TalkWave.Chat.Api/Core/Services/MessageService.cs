using AutoMapper;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Data.MappingProfiles;
using TalkWave.Chat.Data.Repositories;
using TalkWave.Chat.Models.Messages.Request;
using TalkWave.Chat.Models.Messages.Response;

namespace TalkWave.Chat.Api.Core.Services {

    public class MessageService : IMessageService {

        private readonly IMessagesRepository _messageRepository;
        private readonly IChatsRepository _chatsRepository;
        private readonly IMapper _messageMapper;
        
        public MessageService(IMessagesRepository messagesRepository, IChatsRepository chatsRepository , IMapper messageMapper) {

            _chatsRepository = chatsRepository;
            _messageMapper = messageMapper;
            _messageRepository = messagesRepository;

        }

        public async Task<MessageFullResponseModel> GetMessageById(Guid id) {

            try {

                var messageEntity = await _messageRepository.GetByIdAsync(id);

                if (messageEntity == null)
                    throw new Exception("Message not found");

                return _messageMapper.Map<MessageFullResponseModel>(messageEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }

        public async Task<IEnumerable<MessageFullResponseModel>> GetNMessagesFromMessageAsync(GetNMessagesRequestModel model) {

            try {

                if (model.Take <= 0) {

                    return Enumerable.Empty<MessageFullResponseModel>();
                
                }

                var messagesEntity = await _messageRepository.GetNMessagesFromMessageAsync(model.ChatId, model.MessageId, model.Take);

                return _messageMapper.Map<IEnumerable<MessageFullResponseModel>>(messagesEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }


        }

        public async Task<MessageFullResponseModel> CreateMessageAsync(CreateMessageRequestModel model) {
            
            using var transaction = await _messageRepository.BeginTransactionAsync();

            try {

                var messageEntity = _messageMapper.Map<MessageEntity>(model);
                await _messageRepository.AddAsync(messageEntity);

                var chat = await _chatsRepository.GetByIdAsync(model.ChatId);
                if (chat == null) {
                    throw new Exception($"Chat with id {model.ChatId} not found");
                }

                chat.LastMessageId = messageEntity.Id;
                chat.LastMessage = messageEntity;
                await _chatsRepository.UpdateAsync(chat);

                await transaction.CommitAsync();

                return _messageMapper.Map<MessageFullResponseModel>(messageEntity);

            } catch (Exception ex) {

                await transaction.RollbackAsync();
                throw new Exception("Failed to create message", ex);

            }

        }

        public async Task<MessageFullResponseModel> UpdateMessageAsync(UpdateMessageRequestModel model) {

            try {

                var messageEntity = await _messageRepository.GetByIdAsync(model.Id);

                messageEntity.Content = model.Content;
                messageEntity.Status = model.Status;

                await _messageRepository.UpdateAsync(messageEntity);

                return _messageMapper.Map<MessageFullResponseModel>(messageEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }

        public async Task DeleteMessageByIdAsync(Guid id) {

            try {

                var messageEntity = await _messageRepository.GetByIdAsync(id); 

                await _messageRepository.RemoveAsync(messageEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }

    }

}
