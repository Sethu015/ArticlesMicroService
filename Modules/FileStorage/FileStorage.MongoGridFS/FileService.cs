using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS
{
    public class FileService : IFileService
    {
        public FileService(GridFSBucket bucket, IOptions<MongoGridFSFileStorageOptions> options)
        {
        }

        public Task<(Stream FileStream, string ContentType)> DownloadFileAsync(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteFileAsync(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false, Dictionary<string, string>? tags = null)
        {
            throw new NotImplementedException();
        }
    }
}
