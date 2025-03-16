using Microsoft.EntityFrameworkCore;
using TalkWave.User.Data.Entities;
using TalkWave.User.Data.Interfaces;

namespace TalkWave.User.Data.Repositories {

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity {

        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context) {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id) {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity) {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity) {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity) {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }

}
