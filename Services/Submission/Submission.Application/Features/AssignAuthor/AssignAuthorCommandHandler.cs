
namespace Submission.Application.Features.AssignAuthor
{
    public class AssignAuthorCommandHandler(ArticleRepository articleRepository) : IRequestHandler<AssignAuthorCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(AssignAuthorCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetByAsyncOrThrowAsync(request.ArticleId);
            var author = await articleRepository._dbContext.Authors.FindByIdOrThrowAsync(request.AuthorId);
            article.AssignAuthor(author, request.ContributionAreas, request.IsCorrespondingAuthor);
            await articleRepository.SaveChangesAsync();
            return new IdResponse(article.Id);
        }
    }
}
