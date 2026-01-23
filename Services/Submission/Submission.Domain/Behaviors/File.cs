namespace Submission.Domain.Entities.ValueObjects
{
    public partial class File
    {
        private File() { }

        internal static File CreateFile(FileStorage.Contracts.UploadResponse uploadResponse,Asset asset, AssetTypeDefinition assetTypeDefinition)
        {
            var fileName = System.IO.Path.GetFileName(uploadResponse.FileName);
            var extension = FileExtension.Create(fileName, assetTypeDefinition);
            return new File
            {
                OriginalName = fileName,
                FileServerId = uploadResponse.FileId,
                Size = uploadResponse.FileSize,
                Name = FileName.Create(asset,extension),
                Extension = extension
            };
        }
    }
}
