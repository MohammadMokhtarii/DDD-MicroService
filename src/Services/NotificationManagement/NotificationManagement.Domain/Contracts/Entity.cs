using MediatR;

namespace NotificationManagement.Domain.Contracts;

public abstract class Entity
{
    private int _id;
    public virtual int Id
    {
        get { return _id; }
        protected set { _id = value; }
    }

    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification @event)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(@event);
    }

    public void RemoveDomainEvent(INotification @event) => _domainEvents.Remove(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
