using Blocks.Core.Cache;

namespace Submission.Domain.Entities
{
    public class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
    {
        public byte MaxFileSizeInMB { get; set; }
        public int MaxFileSizeInBytes => MaxFileSizeInMB * 1024 * 1024;
        public string DefaultFileExtension { get; set; } = null!;
        public FileExtensions AllowedFileExtensions { get; init; } = null!;
        public int MaxAssetCount { get; init; }
        public bool AllowsMultipleAssets => MaxAssetCount > 1;
    }
}
