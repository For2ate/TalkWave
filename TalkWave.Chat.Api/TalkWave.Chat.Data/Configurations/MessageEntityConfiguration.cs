using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Configurations {

    public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity> {

        public void Configure(EntityTypeBuilder<MessageEntity> builder) {

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Content)
                   .IsRequired()
                   .HasMaxLength(10000); 

            builder.Property(m => m.Status)
                   .IsRequired();

            builder.Property(m => m.SentAt)
                   .IsRequired();

            builder.Property(m => m.SenderId)
                   .IsRequired();

            builder.Property(m => m.ChatId)
                   .IsRequired();

            builder.HasOne(m => m.Sender)
                   .WithMany(u => u.Messages)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Chat)
                   .WithMany(c => c.Messages)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.ChatId)
                .HasDatabaseName("IX_Messages_ChatId");

            builder.HasIndex(m => m.SenderId)
                .HasDatabaseName("IX_Messages_SenderId");

            builder.HasIndex(m => m.SentAt)
                .HasDatabaseName("IX_Messages_SentAt");

            builder.HasIndex(m => new { m.ChatId, m.SentAt })
                .HasDatabaseName("IX_Messages_ChatId_SentAt");

        }

    }

}
