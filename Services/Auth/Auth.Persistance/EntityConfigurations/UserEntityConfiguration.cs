using Auth.Domain.Users;
using Auth.Domain.Users.ValueObjects;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistance.EntityConfigurations
{
    internal class UserEntityConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(MaxLength.C64);

            builder.Property(u => u.LastName).IsRequired().HasMaxLength(MaxLength.C64);
            builder.Property(u => u.Gender).IsRequired().HasEnumConversion();

            builder.OwnsOne(u => u.Honorific, b =>
            {
                b.Property(e => e.Value).HasMaxLength(MaxLength.C32).HasColumnNameAsProperty();

                b.WithOwner();
            });

            builder.OwnsOne(u => u.ProfessionalProfile, b =>
            {
                b.Property(x => x.Position).HasMaxLength(MaxLength.C32).HasColumnNameAsProperty();
                b.Property(x => x.CompanyName).HasMaxLength(MaxLength.C32).HasColumnNameAsProperty();
                b.Property(x => x.Affiliation).HasMaxLength(MaxLength.C32).HasColumnNameAsProperty();
                b.WithOwner();
            });

            builder.Property(u => u.PictureUrl).HasMaxLength(MaxLength.C2048);

            builder.HasMany(u => u.UserRoles).WithOne().HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.RefreshTokens).WithOne().HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
