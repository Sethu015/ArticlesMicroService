using Blocks.Domain.Abstractions;

namespace Blocks.EntityFramework.Repository
{
    public interface IRepository<TEntity> where TEntity : class,IEntity
    {
        Task<TEntity?> FindByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}
