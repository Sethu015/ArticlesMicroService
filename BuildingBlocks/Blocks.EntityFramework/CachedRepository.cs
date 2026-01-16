using Blocks.Core.Cache;
using Blocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFramework
{
    public abstract class CachedRepository<TDbContext, TEntity, TId>(TDbContext dbContext, IMemoryCache memoryCache)
        where TDbContext : DbContext
        where TEntity : class, IEntity<TId>, ICacheable
        where TId : struct
    {
        public IEnumerable<TEntity> GetAll()
            => memoryCache.GetOrCreateByType<IEnumerable<TEntity>>(entry =>
            {
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            })!;

        public TEntity? GetById(TId id)
            => GetAll().Single(e => e.Id.Equals(id));
    }
}
