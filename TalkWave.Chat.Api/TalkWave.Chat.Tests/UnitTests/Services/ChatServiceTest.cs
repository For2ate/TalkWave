using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Reflection;
using TalkWave.Chat.Api.Core.Services;
using TalkWave.Chat.Data.Entities;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Models.ChatMembers;
using TalkWave.Chat.Models.Chats.Request;
using TalkWave.Chat.Models.Chats.Response;
using TalkWave.Common.ChatMember;
using Xunit;

namespace TalkWave.Chat.Tests.UnitTests.Services {

    public class ChatServiceTests {

        private readonly IFixture _fixture;
        private readonly Mock<IChatsRepository> _mockChatsRepository;
        private readonly Mock<IBaseRepository<UserEntity>> _mockUsersRepository;
        private readonly Mock<IMessagesRepository> _mockMessagesRepository;
        private readonly Mock<IChatMembersRepository> _mockChatMembersRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ChatService _chatService;

        public ChatServiceTests() {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(new OmitOnRecursionBehaviorCustomization());

            _mockChatsRepository = _fixture.Freeze<Mock<IChatsRepository>>();
            _mockUsersRepository = _fixture.Freeze<Mock<IBaseRepository<UserEntity>>>();
            _mockMessagesRepository = _fixture.Freeze<Mock<IMessagesRepository>>();
            _mockChatMembersRepository = _fixture.Freeze<Mock<IChatMembersRepository>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();

            _chatService = _fixture.Create<ChatService>();
        }

        private class OmitOnRecursionBehaviorCustomization : ICustomization {
            public void Customize(IFixture fixture) {
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }

        [Fact]
        public async Task GetChatsForUserAsync_ReturnsChatsWithMembers() {

            // Arrange

            var userId = _fixture.Create<Guid>();
            var chats = _fixture.CreateMany<ChatEntity>(3).ToList();

            var chatMembers = chats.Select(chat =>
                _fixture.Build<ChatMemberEntity>()
                    .With(cm => cm.ChatId, chat.Id)
                    .With(cm => cm.UserId, userId)
                    .Create()
            ).ToList();

            _mockChatMembersRepository
                .Setup(repo => repo.GetAllChatsForUserAsync(userId))
                .ReturnsAsync(chatMembers);

            foreach (var chatMember in chatMembers) {
                _mockChatsRepository
                    .Setup(repo => repo.GetChatWithMembersAsync(chatMember.ChatId))
                    .ReturnsAsync(chats.First(c => c.Id == chatMember.ChatId));
            }

            var chatModels = chats.Select(chat =>
                    _fixture.Build<ChatFullResponseModel>()
                        .With(c => c.Id, chat.Id)
                        .With(c => c.Name, chat.Name)
                        .With(c => c.Role, chatMembers.First(cm => cm.ChatId == chat.Id).Role)
                        .Create()
            ).ToList();

            _mockMapper.Setup(mapper => mapper.Map<ChatFullResponseModel>(It.IsAny<ChatEntity>()))
              .Returns((ChatEntity chat) => chatModels.First(m => m.Id == chat.Id));

            // Act
            var result = await _chatService.GetChatsForUserAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(chatModels);

            _mockChatMembersRepository.Verify(repo => repo.GetAllChatsForUserAsync(userId), Times.Once);

            foreach (var chatMember in chatMembers) {
                _mockChatsRepository.Verify(repo => repo.GetChatWithMembersAsync(chatMember.ChatId), Times.Once);
            }

        }

        [Fact]
        public async Task CreatePersonalChatAsync_CreatesNewChat_WhenNoExistingChat() {


            // Arrange
            var model = _fixture.Create<CreatePersonalChatModel>();
            var senderUser = _fixture.Build<UserEntity>()
                .With(u => u.Id, model.SenderUserId)
                .Create();
            var recipientUser = _fixture.Build<UserEntity>()
                .With(u => u.Id, model.RecipientUserId)
                .Create();

            _mockUsersRepository.Setup(x => x.GetByIdAsync(model.SenderUserId))
                .ReturnsAsync(senderUser);
            _mockUsersRepository.Setup(x => x.GetByIdAsync(model.RecipientUserId))
                .ReturnsAsync(recipientUser);

            _mockChatMembersRepository.Setup(x => x.GetCommonChatsForUsersAsync(model.SenderUserId, model.RecipientUserId))
                .ReturnsAsync(new List<ChatEntity>());

            ChatEntity createdChat = null;
            _mockChatsRepository.Setup(x => x.AddAsync(It.IsAny<ChatEntity>()))
                .Callback<ChatEntity>(chat => createdChat = chat)
                .Returns(Task.CompletedTask);

            var expectedResponse = _fixture.Create<ChatFullResponseModel>();
            _mockMapper.Setup(x => x.Map<ChatFullResponseModel>(It.IsAny<ChatEntity>()))
                .Returns(expectedResponse);

            // Act
            var result = await _chatService.CreatePersonalChatAsync(model);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);

            // Проверяем создание чата
            _mockChatsRepository.Verify(x => x.AddAsync(It.IsAny<ChatEntity>()), Times.Once);

            // Проверяем, что чат содержит двух участников
            createdChat.Should().NotBeNull();
            createdChat.Members.Should().HaveCount(2);
            createdChat.Members.Should().Contain(m => m.UserId == model.SenderUserId);
            createdChat.Members.Should().Contain(m => m.UserId == model.RecipientUserId);

        }

        [Fact]
        public async Task CreatePersonalChatAsync_ReturnsExistingChat_WhenFound() {

            // Arrange
            var model = _fixture.Build<CreatePersonalChatModel>()
                .With(x => x.Message, "Test message")
                .Create();

            var existingChat = _fixture.Build<ChatEntity>()
                .With(c => c.Members, new List<ChatMemberEntity>
                {
                    new ChatMemberEntity { UserId = model.SenderUserId, Role = ChatMemberRole.Owner },
                    new ChatMemberEntity { UserId = model.RecipientUserId, Role = ChatMemberRole.Member }
                })
                .With(c => c.IsGroupChat, false)
                .Create();

            var senderUser = _fixture.Build<UserEntity>()
                .With(u => u.Id, model.SenderUserId)
                .Create();
            var recipientUser = _fixture.Build<UserEntity>()
                .With(u => u.Id, model.RecipientUserId)
                .Create();

            _mockChatMembersRepository
                .Setup(repo => repo.GetCommonChatsForUsersAsync(model.SenderUserId, model.RecipientUserId))
                .ReturnsAsync(new List<ChatEntity> { existingChat });

            _mockUsersRepository
                .Setup(repo => repo.GetByIdAsync(senderUser.Id))
                .ReturnsAsync(senderUser);

            _mockUsersRepository
                 .Setup(repo => repo.GetByIdAsync(recipientUser.Id))
                 .ReturnsAsync(recipientUser);

            _mockChatsRepository
                .Setup(repo => repo.GetChatWithMembersAsync(existingChat.Id))
                .ReturnsAsync(existingChat);

            var expectedResponse = new ChatFullResponseModel {
                Id = existingChat.Id,
                Name = existingChat.Name,
                CreatedAt = existingChat.CreatedAt,
                CreatedBy = existingChat.CreatedBy,
                IsGroupChat = existingChat.IsGroupChat,
                LastMessageId = existingChat.LastMessageId.Value,
                Role = ChatMemberRole.Owner,
                ChatMembers = existingChat.Members.Select(m => new ChatMemberModel {
                    Id = m.UserId,
                    Role = m.Role
                }).ToList()
            };

            _mockMapper.Setup(mapper => mapper.Map<ChatFullResponseModel>(existingChat))
                      .Returns(expectedResponse);

            // Act
            var result = await _chatService.CreatePersonalChatAsync(model);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
            _mockChatsRepository.Verify(repo => repo.AddAsync(It.IsAny<ChatEntity>()), Times.Never);
            _mockMessagesRepository.Verify(repo => repo.AddAsync(It.IsAny<MessageEntity>()), Times.Never);

        }

        [Fact]
        public async Task CreatePersonalChatAsync_CreatesInitialMessage_WhenProvided() {

            // Arrange
            var model = _fixture.Build<CreatePersonalChatModel>()
                .With(x => x.Message, "Test message")
                .Create();

            _mockChatMembersRepository.Setup(x => x.GetCommonChatsForUsersAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new List<ChatEntity>());

            // Act
            await _chatService.CreatePersonalChatAsync(model);

            // Assert
            _mockMessagesRepository.Verify(x => x.AddAsync(It.Is<MessageEntity>(m =>
                m.Content == model.Message &&
                m.Status == Common.Message.MessageStatus.Sent)),
                Times.Once);

        }

        [Fact]
        public async Task CreatePersonalChatAsync_RollsBack_OnError() {

            // Arrange
            var model = _fixture.Create<CreatePersonalChatModel>();
            var mockTransaction = new Mock<IDbContextTransaction>();

            _mockChatsRepository.Setup(x => x.BeginTransactionAsync())
                .ReturnsAsync(mockTransaction.Object);

            _mockUsersRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Test error"));

            mockTransaction.Setup(x => x.RollbackAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockTransaction.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _chatService.CreatePersonalChatAsync(model));

            // Проверяем вызовы с любым CancellationToken
            mockTransaction.Verify(
                x => x.RollbackAsync(It.IsAny<CancellationToken>()),
                Times.Once);

            mockTransaction.Verify(
                x => x.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Never);


        }

        private async Task<UserEntity> InvokeGetOrCreateUserAsync(Guid userId) {
            var method = typeof(ChatService)
                .GetMethod("GetOrCreateUserAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return await (Task<UserEntity>)method.Invoke(_chatService, new object[] { userId });
        }

        [Fact]
        public async Task GetOrCreateUserAsync_ReturnsExistingUser() {

            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = _fixture.Build<UserEntity>()
                .With(x => x.Id, userId)
                .Create();

            _mockUsersRepository.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);

            // Act
            var result = await InvokeGetOrCreateUserAsync(userId);

            // Assert
            result.Should().Be(existingUser);
            _mockUsersRepository.Verify(x => x.AddAsync(It.IsAny<UserEntity>()), Times.Never);

        }

        [Fact]
        public async Task GetOrCreateUserAsync_CreatesNewUser_WhenNotFound() {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUsersRepository.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync((UserEntity?)null);

            // Act
            var result = await InvokeGetOrCreateUserAsync(userId);

            // Assert
            result.Id.Should().Be(userId);
            _mockUsersRepository.Verify(x => x.AddAsync(It.Is<UserEntity>(u => u.Id == userId)), Times.Once);
        }

    }
}