namespace Submission.Domain.Entities.ValueObjects
{
    public class FileName : StringValueObject
    {
        private FileName(string value) => Value = value;
        public static FileName Create(Asset asset,FileExtension fileExtension)
        {
            var assetName = asset.Name.Value;
            // Add validation logic here if needed
            return new FileName($"{assetName}.{fileExtension}");
        }
    }
}
