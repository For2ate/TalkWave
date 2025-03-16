using Microsoft.EntityFrameworkCore;
using TalkWave.User.Data.Configurations;
using TalkWave.User.Data.Entities;

namespace TalkWave.User.Data.DataContexts {

    public class UserContext : DbContext {

        public DbSet<UserEntity> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> contextOptions) : base(contextOptions) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            base.OnModelCreating(modelBuilder);

        }

    }

}
