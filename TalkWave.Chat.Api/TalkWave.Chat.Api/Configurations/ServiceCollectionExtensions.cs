using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Api.Core.Interfaces;
using TalkWave.Chat.Api.Core.Services;
using TalkWave.Chat.Data.Contexts;
using TalkWave.Chat.Data.Interfaces;
using TalkWave.Chat.Data.MappingProfiles;
using TalkWave.Chat.Data.Repositories;

namespace TalkWave.Chat.Api.Configurations {

    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            // Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IChatsRepository, ChatsRepository>();
            services.AddScoped<IChatMembersRepository, ChatMembersRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();

            // Services
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();

            return services;
        }


        public static IServiceCollection AddApplicationAutoMapper(this IServiceCollection services) {

            services.AddAutoMapper(typeof(ChatMappingProfile));
            services.AddAutoMapper(typeof(MessageMappingProfile));
            return services;

        }

    }

}
