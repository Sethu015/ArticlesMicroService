using Blocks.Domain.Abstractions;
using Blocks.EntityFramework.Repository;

namespace Submission.Persistance.Repository
{
    public class Repository<TEntity> : Repository<SubmissionDbContext,TEntity>
        where TEntity : class,IEntity
    {
        public SubmissionDbContext _dbContext;

        public Repository(SubmissionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
