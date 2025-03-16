using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TalkWave.User.Data.Entities;

namespace TalkWave.User.Data.Configurations {

    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity> {

        public void Configure(EntityTypeBuilder<UserEntity> builder) {

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Login)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(u => u.Password)
                    .IsRequired();

            builder.Property(u => u.Email)
                    .IsRequired();

            builder.Property(u => u.FirstName)
                    .IsRequired();

            builder.Property(u => u.LastName);

            builder.HasIndex(u => u.Login)
                   .IsUnique();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

        }

    }

}
