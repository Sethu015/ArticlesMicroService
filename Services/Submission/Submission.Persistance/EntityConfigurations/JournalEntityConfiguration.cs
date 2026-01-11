using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations;

internal class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(64);

        builder.Property(a => a.Abbrevation).IsRequired().HasMaxLength(8);
    }
}
