namespace Features.Customers.Domain.Events;

public class CustomerUpdatedEvent : DomainEvent
{
    public Guid Id { get; set; }
    public string OldName { get; set; }
    public string NewName { get; set; }
}