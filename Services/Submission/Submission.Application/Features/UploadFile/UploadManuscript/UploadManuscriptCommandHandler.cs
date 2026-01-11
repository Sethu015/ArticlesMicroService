
namespace Submission.Application.Features.UploadFile.UploadManuscript
{
    public class UploadManuscriptCommandHandler(ArticleRepository articleRepository) : IRequestHandler<UploadManuscriptCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(UploadManuscriptCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetByAsyncOrThrowAsync(request.ArticleId);
            return new IdResponse(0);
        }
    }
}
