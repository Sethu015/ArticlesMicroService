using Blocks.Exceptions;

namespace Submission.Domain.Entities
{
    public partial class Article
    {
        public void AssignAuthor(Author author, HashSet<ContributionAreas> contributionAreas, bool isCorrespondingAuthor)
        {
            var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;
            if (_actors.Exists(a => a.PersonId == author.Id && a.Role == role))
                throw new DomainException($"Author {author.EmailAddress} is already assigned to the article.");
            _actors.Add(new ArticleAuthor()
            {
                Person = author,
                Role = role,
                ContributionAreas = contributionAreas
            });
        }
    }
}
