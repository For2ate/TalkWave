using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Configurations {

    public class ChatEntityConfiguration : IEntityTypeConfiguration<ChatEntity> {

        public void Configure(EntityTypeBuilder<ChatEntity> builder) {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(c => c.IsGroupChat)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.Property(c => c.CreatedBy)
                   .IsRequired();

            builder.Property(c => c.LastMessageId)
                   .IsRequired(); 

            builder.HasOne(c => c.LastMessage)
                   .WithOne()
                   .HasForeignKey<ChatEntity>(c => c.LastMessageId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Messages)
                   .WithOne(m => m.Chat)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.CreatedAt)
                .HasDatabaseName("IX_Chats_CreatedAt");

            builder.HasIndex(c => c.IsGroupChat)
                .HasDatabaseName("IX_Chats_IsGroupChat");

            builder.HasIndex(c => c.LastMessageId)
                .HasDatabaseName("IX_Chats_LastMessageId");

        }

    }

}
