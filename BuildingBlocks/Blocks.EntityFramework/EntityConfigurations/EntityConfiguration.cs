using Blocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFramework.EntityConfigurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class,IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().HasColumnOrder(0);
        }
    }
}
