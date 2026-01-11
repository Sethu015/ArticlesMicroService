namespace Blocks.Domain.Abstractions
{
    public interface IAuditableAction
    {
        DateTime CreatedOn => DateTime.UtcNow;
        int CreatedById { get; set; }
    }

    public interface IAuditableAction<TActionType> : IAuditableAction where TActionType : Enum
    {
        TActionType ActionType { get; }
    }
}
