using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.SharedKernel;

public abstract class HasDomainEvent
{
    private readonly List<IDomainEvent> _domainEvents = [];
    [NotMapped]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}