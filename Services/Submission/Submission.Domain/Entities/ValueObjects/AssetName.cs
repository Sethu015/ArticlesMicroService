using Articles.Abstractions.Enums;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.Entities.ValueObjects
{
    public class AssetName : StringValueObject
    {
        private AssetName(string value) => Value = value;

        public static AssetName FromAssetType(AssetType value)
        {
            // Add validation logic here if needed
            return new AssetName(value.ToString());
        }
    }
}
