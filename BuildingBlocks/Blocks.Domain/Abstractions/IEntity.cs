namespace Blocks.Domain.Abstractions
{
    public interface IEntity
    {
        int Id { get; }
    }

    public interface IEntity<T> where T : struct
    {
        T Id { get; }
    }

    public abstract class Entity : IEntity
    {
        public virtual int Id { get; init; }
    }

    public abstract class Entity<T> : IEntity<T> where T : struct
    {
        public virtual T Id { get; init; } = default!;
    }
}
