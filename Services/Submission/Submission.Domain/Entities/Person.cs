namespace Submission.Domain.Entities;

public class Person : IEntity
{
    public int Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string FullName => $"{FirstName} {LastName}";
    public string? Title { get; init; }
    public required EmailAddress EmailAddress { get; init; }
    public required string Affiliation { get; init; }
    public int? UserId { get; init; }

    public string TypeDiscriminator { get; set; } = null!; //EF Core Discriminator
    public IReadOnlyList<ArticleActor> ArticleActors { get; init; } = new List<ArticleActor>();
}
