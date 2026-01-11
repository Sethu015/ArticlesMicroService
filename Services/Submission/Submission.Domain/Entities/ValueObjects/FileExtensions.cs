using Blocks.Core.Extensions;

namespace Submission.Domain.Entities.ValueObjects
{
    public class FileExtensions
    {
        public IReadOnlyCollection<string> Extensions { get; init; } = null!;

        public bool IsValidExtension(string extension)
        {
            //Note If the extension is empty then all extensions are allowed
            return Extensions.IsEmpty() || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
