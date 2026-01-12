using Blocks.Core.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Submission.Domain.Entities.ValueObjects.File;

namespace Submission.Persistance.EntityConfigurations
{
    internal class FileEntityConfiguration
    {
        public void Configure(ComplexPropertyBuilder<File> builder)
        {
            builder.Property(f => f.OriginalName).HasMaxLength(MaxLength.C256).HasComment("Original full Filename, with Extension");

            builder.Property(f => f.FileServerId).HasMaxLength(MaxLength.C64);

            builder.Property(f => f.Size).HasComment("Size of file in Kilobytes");

            builder.ComplexProperty(f => f.Name, b =>
            {
                b.Property(f => f.Value).HasColumnName($"{b.Metadata.ClrType.Name}_{b.Metadata.PropertyInfo!.Name}")
                .HasMaxLength(MaxLength.C64).HasComment("File name of the file after renaming");
            });

            builder.ComplexProperty(f => f.Extension, b =>
            {
                b.Property(f => f.Value).HasColumnName($"{b.Metadata.ClrType.Name}_{b.Metadata.PropertyInfo!.Name}")
                .HasMaxLength(MaxLength.C8);
            });

        }
    }
}
