using Blocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework.Repository
{
    public class Repository<TDbContext,TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly DbSet<TEntity> _entity;

        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = dbContext.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> Query() => _entity;
        public string TableName => _dbContext.Model.FindEntityType(typeof(TEntity))!.GetTableName()!;

        public async Task<TEntity> AddAsync(TEntity entity) => (await _entity.AddAsync(entity)).Entity;

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
            return rowsAffected > 0;
        }

        public async Task<TEntity?> GetByIdAsync(int id) => await Query().SingleOrDefaultAsync(e => e.Id == id);

        public TEntity Remove(TEntity entity) => _entity.Remove(entity).Entity;

        public TEntity Update(TEntity entity) => _entity.Update(entity).Entity;
        public async Task<TEntity?> FindByIdAsync(int id) => await _entity.FindAsync(id);
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
