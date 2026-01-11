namespace Submission.Domain.Entities.ValueObjects
{
    public class FileExtension : StringValueObject
    {
        private FileExtension(string value) => Value = value;
        public static FileExtension Create(string fileName, AssetTypeDefinition assetType)
        {
            var extension = Path.GetExtension(fileName).Remove(0,1);
            Guard.ThrowIfNullOrWhiteSpace(extension);
            Guard.ThrowIfNotEqual(
                assetType.AllowedFileExtensions.IsValidExtension(extension), true
            );
            // Add validation logic here if needed
            return new FileExtension(extension);
        }
    }
}
