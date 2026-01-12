using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations
{
    internal class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
    {
        public void Configure(EntityTypeBuilder<ArticleActor> builder)
        {
            builder.HasKey(aa => new { aa.ArticleId, aa.PersonId, aa.Role });

            builder.HasDiscriminator(aa => aa.TypeDiscriminator)
                .HasValue<ArticleActor>(nameof(ArticleActor))
                .HasValue<ArticleAuthor>(nameof(ArticleAuthor));

            builder.Property(aa => aa.Role).HasEnumConversion().HasDefaultValue(UserRoleType.AUT);

            builder.HasOne(aa => aa.Article)
                .WithMany(a => a.Actors)
                .HasForeignKey(aa => aa.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(aa => aa.Person)
                .WithMany(p => p.ArticleActors)
                .HasForeignKey(aa => aa.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
