﻿using AutoMapper;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Data.MappingProfiles;
using TalkWave.Chat.Models.Messages.Request;
using TalkWave.Chat.Models.Messages.Response;

namespace TalkWave.Chat.Api.Core.Services {

    public class MessageService : IMessageService {

        private readonly IMessagesRepository _messageRepository;
        private readonly IMapper _messageMapper;
        
        public MessageService(IMessagesRepository messagesRepository, IMapper messageMapper) {

            _messageMapper = messageMapper;
            _messageRepository = messagesRepository;

        }

        public async Task<MessageFullResponseModel> GetMessageById(Guid id) {

            try {

                var messsageEntity = await _messageRepository.GetByIdAsync(id);

                return _messageMapper.Map<MessageFullResponseModel>(messsageEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }

        public async Task<MessageFullResponseModel> CreateMessageAsync(CreateMessageRequestModel model) {

            try {

                var messageEntity = _messageMapper.Map<MessageEntity>(model);

                await _messageRepository.AddAsync(messageEntity);

                return _messageMapper.Map<MessageFullResponseModel>(messageEntity);

            } catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }

        public async Task<MessageFullResponseModel> UpdateMessageAsync(UpdateMessageRequestModel model) {

            try {

                var messageEntity = await _messageRepository.GetByIdAsync(model.Id);

                messageEntity.Content = model.Content;

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
