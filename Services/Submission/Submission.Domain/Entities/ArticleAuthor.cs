namespace Submission.Domain.Entities;
public class ArticleAuthor : ArticleActor
{
    public HashSet<ContributionAreas> ContributionAreas { get; init; } = null!;
}
