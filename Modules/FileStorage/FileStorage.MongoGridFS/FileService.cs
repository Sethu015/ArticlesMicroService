using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS
{
    public class FileService : IFileService
    {
        private const string FilePathMetaDataKey = "filePath";
        private const string ContentTypeMetaDataKey = "contentType";
        private const string DefaultContentType = "application/octet-stream";

        private readonly GridFSBucket _bucket;
        private readonly IOptions<MongoGridFSFileStorageOptions> _options;

        public FileService(GridFSBucket bucket, IOptions<MongoGridFSFileStorageOptions> options)
        {
            _bucket = bucket;
            _options = options;
        }

        public async Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
        {
            if(!ObjectId.TryParse(fileId, out var objectId))
                throw new FileNotFoundException($"Invalid file ID. {fileId}");

            var fileInfo = await _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
            if (fileInfo == null)
                throw new FileNotFoundException($"File with ID {fileId} not found.");

            var stream = await _bucket.OpenDownloadStreamAsync(objectId);
            var contentType = fileInfo.Metadata?.GetValue(ContentTypeMetaDataKey,DefaultContentType)?.AsString ?? DefaultContentType;
            return (stream, contentType);
        }

        public Task<bool> TryDeleteFileAsync(string fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
        {
            if(file.Length > _options.Value.FileSizeLimitInBytes)
                throw new InvalidOperationException($"File size exceeds the limit of {_options.Value.FileSizeLimitInBytes} bytes.");

            var metaData = new BsonDocument(tags ?? new Dictionary<string, string>())
            {
                { FilePathMetaDataKey, filePath  },
                { ContentTypeMetaDataKey, file.ContentType }
            };

            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = metaData,
                ChunkSizeBytes = _options.Value.ChunkSizeInBytes
            };

            ObjectId fileId;
            using (var stream = file.OpenReadStream())
            {
                fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, uploadOptions);
            }
            return new UploadResponse(
                FileId : fileId.ToString(),
                FileName : file.FileName,
                FilePath : filePath,
                FileSize : file.Length
            );
        }
    }
}
