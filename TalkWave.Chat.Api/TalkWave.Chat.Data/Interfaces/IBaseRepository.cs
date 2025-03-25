using TalkWave.Chat.Data.Entities;

namespace TalkWave.Chat.Data.Interfaces {

    public interface IBaseRepository<TEntity> where TEntity: BaseEntity {

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

    }

}
