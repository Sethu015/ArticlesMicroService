using Blocks.Core.FluentValidation;
using Microsoft.AspNetCore.Http;
using Submission.Application.Features._Shared;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile
{
    public record UploadManuscriptCommand : ArticleCommand
    {
        /// <summary>
        /// The asset type of the file.
        /// </summary>
        [Required]
        public AssetType AssetType { get; init; }

        /// <summary>
        /// The file to be uploaded.
        /// </summary>
        [Required]
        public IFormFile File { get; init; } = null!;

        public override ArticleActionType ActionType => ArticleActionType.Upload;
    }

    public class UploadManuscriptCommandValidator : ArticleCommandValidator<UploadManuscriptCommand>
    {
        public UploadManuscriptCommandValidator()
        {
            RuleFor(c => c.File)
                .NotNullWithMessage(nameof(UploadManuscriptCommand.File));
            RuleFor(c => c.AssetType)
                .Must(IsValidAssetType)
                .WithMessage(c => $"{c.AssetType} is not a valid asset type.");
        }

        private IReadOnlyCollection<AssetType> AllowedAssetTypes = new HashSet<AssetType> { AssetType.Manuscript };
        private bool IsValidAssetType(AssetType assetType) => AllowedAssetTypes.Contains(assetType);
    }
}
