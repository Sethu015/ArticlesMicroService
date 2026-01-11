namespace Submission.Domain.Entities
{
    public partial class Article : IEntity
    {
        public int Id { get; init; }
        public required string Title { get; set; }
        public required string Scope { get; set; }
        public ArticleType ArticleType { get; set; }
        public ArticleStage ArticleStage { get; internal set; }
        public int JournalId { get; init; }
        public required Journal Journal { get; init; }
        public IReadOnlyList<ArticleActor> Actors => _actors.AsReadOnly();
        private readonly List<ArticleActor> _actors = new();
    }
}
