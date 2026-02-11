using Auth.Domain.Roles;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistance.EntityConfigurations
{
    public class RoleEntityConfiguration : EntityConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(r => r.Type).IsRequired().HasEnumConversion();
            builder.Property(r => r.Description).IsRequired().HasMaxLength(MaxLength.C256);
        }
    }
}
