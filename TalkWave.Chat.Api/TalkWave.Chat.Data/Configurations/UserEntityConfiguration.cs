using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Configurations {

    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity> {

        public void Configure(EntityTypeBuilder<UserEntity> builder) {

            builder.HasKey(u => u.Id);

        }

    }

}
