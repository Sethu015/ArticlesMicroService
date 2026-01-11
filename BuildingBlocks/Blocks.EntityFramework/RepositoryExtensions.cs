using Blocks.Domain.Abstractions;
using Blocks.EntityFramework.Repository;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework
{
    public static class RepositoryExtensions
    {
        public static async Task<TEntity> FindByAsyncOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
            where TEntity : class, IEntity
            where TContext : DbContext
        {
            var entity = await repository.FindByIdAsync(id);
            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} was not found.");
            }
            return entity;
        }

        public static async Task<TEntity> GetByAsyncOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
            where TEntity : class, IEntity
            where TContext : DbContext
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} was not found.");
            }
            return entity;
        }

        public static async Task<TEntity> FindByIdOrThrowAsync<TEntity>(this DbSet<TEntity> dbSet, int id)
            where TEntity : class, IEntity
        {
            var entity = await dbSet.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name} was not found.");
            }
            return entity;
        }
    }
}
