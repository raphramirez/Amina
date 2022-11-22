namespace Amina.Domain.Common.Models;

public interface IAggregateRoot
{
}

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : notnull
{
    protected AggregateRoot(TId id)
        : base(id)
    {
    }
}