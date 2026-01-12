using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations;

internal class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Title).IsRequired().HasMaxLength(256);

        builder.Property(a => a.Scope).IsRequired().HasMaxLength(2048);

        builder.Property(a => a.ArticleStage).HasEnumConversion();

        builder.Property(a => a.ArticleType).HasEnumConversion();

        builder.HasOne(a => a.Journal)
               .WithMany(j => j.Articles)
               .HasForeignKey(a => a.JournalId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Assets)
            .WithOne(a => a.Article)
            .HasForeignKey(a => a.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
