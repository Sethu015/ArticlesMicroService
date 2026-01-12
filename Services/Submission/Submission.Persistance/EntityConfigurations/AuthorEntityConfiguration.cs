using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistance.EntityConfigurations
{
    internal class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Degree).HasMaxLength(64)
                .HasComment("The author's highest academic qualification (eg: PHD in Mathematics, MSC in Chemistry)");

            builder.Property(a => a.Discipline).HasMaxLength(64)
                .HasComment("The author's main field of study or research (eg: Biology, ComputerScience)");
        }
    }
}
