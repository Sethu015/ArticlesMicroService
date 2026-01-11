namespace Blocks.Domain.Abstractions
{
    public class EnumEntity<TEnum> : Entity<TEnum> where TEnum : struct,Enum
    {
        public TEnum Name { get; init; } = default!;
    }
}
