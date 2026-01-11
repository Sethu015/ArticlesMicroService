using Microsoft.EntityFrameworkCore;

namespace Submission.Application.Features.CreateArticle
{
    internal class CreateArticleCommandHandler(Repository<Journal> repository) : IRequestHandler<CreateArticleCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var journal = await repository.FindByAsyncOrThrowAsync(command.JournalId);
            var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);
            await AssignAuthor(article, command);
            await repository.SaveChangesAsync();
            return new IdResponse(article.Id);
        }

        private async Task AssignAuthor(Article article,CreateArticleCommand createArticleCommand)
        {
            var author = await repository._dbContext.Authors.SingleOrDefaultAsync(a => a.UserId == createArticleCommand.CreatedById);
            if(author is not null)
                article.AssignAuthor(author, [ContributionAreas.OriginalDraft], isCorrespondingAuthor: true);
        }
    }
}
