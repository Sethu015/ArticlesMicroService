using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations
{
    public class ArticleAuthorEntityConfiguration : IEntityTypeConfiguration<ArticleAuthor>
    {
        public void Configure(EntityTypeBuilder<ArticleAuthor> builder)
        {
            builder.Property(aa => aa.ContributionAreas).HasJsonCollectionConversion().IsRequired();
        }
    }
}
