using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations
{
    internal class AssetEntityConfiguration : EntityConfiguration<Asset>
    {
        public override void Configure(EntityTypeBuilder<Asset> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Type).HasEnumConversion();

            builder.ComplexProperty(a => a.Name, b =>
            {
                b.Property(n => n.Value).HasColumnName(b.Metadata.PropertyInfo!.Name);
                b.Property(n => n.Value).HasMaxLength(MaxLength.C64).IsRequired();
            });

            builder.ComplexProperty(a => a.File, b =>
            {
                new FileEntityConfiguration().Configure(b);
            });
        }
    }
}
