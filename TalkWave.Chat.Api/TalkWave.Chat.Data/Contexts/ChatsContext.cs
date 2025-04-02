using Microsoft.EntityFrameworkCore;
using TalkWave.Chat.Data.Configurations;
using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Contexts {

    public class ChatsContext : DbContext {

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<ChatMemberEntity> ChatMembers { get; set; }

        public DbSet<ChatEntity> Chats { get; set; }

        public ChatsContext(DbContextOptions<ChatsContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMemberEntityConfiguration());

        }


    }

}