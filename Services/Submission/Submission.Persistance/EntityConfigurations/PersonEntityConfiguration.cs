using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations
{
    public class PersonEntityConfiguration : EntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.UserId).IsUnique();

            builder.HasDiscriminator(p => p.TypeDiscriminator)
                .HasValue<Person>(nameof(Person))
                .HasValue<Author>(nameof(Author));

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.Title).HasMaxLength(64);
            builder.Property(p => p.Affiliation).IsRequired().HasMaxLength(512)
                .HasComment("The organization or institution they are associated with when they conduct the research.");
            builder.Property(p => p.UserId).IsRequired(false);

            builder.ComplexProperty(p => p.EmailAddress, builder =>
            {
                builder.Property(e => e.Value)
                    .HasMaxLength(64)
                    .HasColumnName(builder.Metadata.PropertyInfo!.Name);
            });
        }
    }
}
