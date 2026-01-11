using Submission.Domain.Entities;

namespace Submission.Persistance.Repository
{
    public class ArticleRepository(SubmissionDbContext submissionDbContext) : Repository<Article>(submissionDbContext)
    {
    }
}
