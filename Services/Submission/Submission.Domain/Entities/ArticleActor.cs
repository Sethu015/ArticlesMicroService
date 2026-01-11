namespace Submission.Domain.Entities;
public class ArticleActor
{
    public int ArticleId { get; init; }
    public Article Article { get; init; }
    public int PersonId { get; init; }
    public Person Person { get; init; }
    public UserRoleType Role { get; init; }
    public string TypeDiscriminator { get; set; } = null!; //EF Core Discriminator
}
