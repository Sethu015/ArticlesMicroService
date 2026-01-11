
namespace Submission.Application.Features.CreateAndAssignAuthor
{
    public class CreateAndAssignAuthorCommandHandler(ArticleRepository articleRepository) : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetByAsyncOrThrowAsync(request.ArticleId);

            Author? author = null;
            if(request.UserId is null)
            {
                author = Author.Create(request.Email!, request.FirstName!, request.LastName!, request.Title!, request.Affliation!);
            }
            else
            {

            }
            article.AssignAuthor(author, request.ContributionAreas, request.IsCorrespondingAuthor);
            await articleRepository.SaveChangesAsync();
            return new IdResponse(article.Id);
        }
    }
}
