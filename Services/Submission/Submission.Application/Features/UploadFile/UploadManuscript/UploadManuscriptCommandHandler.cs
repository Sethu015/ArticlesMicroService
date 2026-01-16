
namespace Submission.Application.Features.UploadFile.UploadManuscript
{
    public class UploadManuscriptCommandHandler(ArticleRepository articleRepository,AssetTypeDefinitionRepository repository) : IRequestHandler<UploadManuscriptCommand, IdResponse>
    {
        public async Task<IdResponse> Handle(UploadManuscriptCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetByAsyncOrThrowAsync(request.ArticleId);
            var assetTypeDefenition = repository.GetById(request.AssetType);

            Asset asset = null;
            if(!assetTypeDefenition.AllowsMultipleAssets)
                asset = article.Assets.SingleOrDefault(a => a.Type == assetTypeDefenition.Id);

            if (asset is null)
                asset = article.CreateAsset(assetTypeDefenition);
            await articleRepository.SaveChangesAsync();

            return new IdResponse(asset.Id);
        }
    }
}
