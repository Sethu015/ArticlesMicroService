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

        public Asset  CreateAsset(AssetTypeDefinition assetTypeDefinition)
        {
            var assetCount = _assets.Count(a => a.Type == assetTypeDefinition.Id);
            if (assetTypeDefinition.MaxAssetCount < assetCount)
                throw new DomainException($"The maximum number of files allowed for {assetTypeDefinition.Name.ToString()} was already reached");
            Asset asset = Asset.Create(this, assetTypeDefinition);
            _assets.Add(asset);
            return asset;
        }
    }
}
