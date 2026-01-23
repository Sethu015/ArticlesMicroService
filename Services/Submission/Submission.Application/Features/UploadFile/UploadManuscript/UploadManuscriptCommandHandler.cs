
using FileStorage.Contracts;

namespace Submission.Application.Features.UploadFile.UploadManuscript
{
    public class UploadManuscriptCommandHandler(ArticleRepository articleRepository,AssetTypeDefinitionRepository repository,IFileService fileStorage) : IRequestHandler<UploadManuscriptCommand, IdResponse>
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

            var filePath = asset.GenerateStorageFilePath(request.File.FileName);
            var response = await fileStorage.UploadFileAsync(filePath, request.File, overwrite: true, tags: new Dictionary<string, string>()
            {
                { "entity",nameof(Asset) },
                { "entityId",asset.Id.ToString()  }
            });

            try
            {
                asset.CreateFile(response, assetTypeDefenition);
                await articleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await fileStorage.TryDeleteFileAsync(response.FileId);
                throw;
            }

            return new IdResponse(asset.Id);
        }
    }
}
