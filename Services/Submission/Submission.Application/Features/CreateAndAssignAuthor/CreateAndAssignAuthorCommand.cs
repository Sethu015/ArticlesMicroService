using Blocks.Core.Constraints;
using Blocks.Core.FluentValidation;
using Submission.Application.Features._Shared;

namespace Submission.Application.Features.CreateAndAssignAuthor
{
    public record CreateAndAssignAuthorCommand(int? UserId, string? FirstName, string? LastName, string? Email, string? Title, string? Affliation, bool IsCorrespondingAuthor, HashSet<ContributionAreas> ContributionAreas) : ArticleCommand
    {
        public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
    }

    public class CreateAndAssignAuthorCommandValidator: ArticleCommandValidator<CreateAndAssignAuthorCommand>
    {
        public CreateAndAssignAuthorCommandValidator()
        {
            When(c => c.UserId is null, () =>
            {
                RuleFor(x => x.Email)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Email))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.Email));

                RuleFor(x => x.FirstName)
                    .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.FirstName))
                    .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.FirstName));

                RuleFor(x => x.LastName)
                    .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.LastName))
                    .MaximumLengthWithMessage(MaxLength.C256, nameof(CreateAndAssignAuthorCommand.LastName));

                RuleFor(x => x.Affliation)
                    .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Affliation))
                    .MaximumLengthWithMessage(MaxLength.C512, nameof(CreateAndAssignAuthorCommand.Affliation));
            });

            RuleFor(x => x.ContributionAreas)
            .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.ContributionAreas));
        }
    }
}
