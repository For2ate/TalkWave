using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Configurations {

    public class ChatMemberEntityConfiguration : IEntityTypeConfiguration<ChatMemberEntity> {

        public void Configure(EntityTypeBuilder<ChatMemberEntity> builder) {


            builder.HasKey(cm => cm.Id);

            builder.Property(cm => cm.UserId)
                   .IsRequired();

            builder.Property(cm => cm.ChatId)
                   .IsRequired();

            builder.Property(cm => cm.Role)
                   .IsRequired();

            builder.HasOne(cm => cm.User)
                   .WithMany(u => u.ChatMembers)
                   .HasForeignKey(cm => cm.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.Chat)
                   .WithMany(c => c.Members)
                   .HasForeignKey(cm => cm.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cm => cm.UserId)
                .HasDatabaseName("IX_ChatMembers_UserId");

            builder.HasIndex(cm => cm.ChatId)
                .HasDatabaseName("IX_ChatMembers_ChatId");

            builder.HasIndex(cm => new { cm.UserId, cm.ChatId })
                .IsUnique()
                .HasDatabaseName("IX_ChatMembers_UserId_ChatId");

        }

    }

}
