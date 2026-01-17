namespace FileStorage.MongoGridFS
{
    public class MongoGridFSFileStorageOptions
    {
        public string DatabaseName { get; set; } = default!;
        public string BucketName { get; set; } = "files";
        public int ChunkSizeInBytes { get; set; } = 1048576; // 1 MB
        public int FileSizeLimitInMB { get; set; } = 50; // 50 MB
        public long FileSizeLimitInBytes => FileSizeLimitInMB * 1024 * 1024;
    }
}
