namespace Wavy.Domain.Core;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> domainEvents = new();
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => domainEvents.ToList();
    public void ClearDomainEvents() => domainEvents.Clear();
    protected void AddDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}