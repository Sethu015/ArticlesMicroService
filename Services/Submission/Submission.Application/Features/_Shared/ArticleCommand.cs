using Blocks.Core.FluentValidation;

namespace Submission.Application.Features._Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, IRequest<TResponse> where TActionType : Enum
{
    [JsonIgnore]
    public int ArticleId { get; init; }
    public string? Comment { get; init; }
    [JsonIgnore]
    public abstract TActionType ActionType { get; }
    [JsonIgnore]
    public string Action => ActionType.ToString();
    [JsonIgnore]
    public DateTime CreatedOn => DateTime.UtcNow;
    [JsonIgnore]
    public int CreatedById { get; set; }
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType,IdResponse>;

public abstract class ArticleCommandValidator<IFileActionCommand>: AbstractValidator<IFileActionCommand> where IFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(c => c.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }
}