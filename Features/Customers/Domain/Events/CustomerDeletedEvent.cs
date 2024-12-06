namespace Features.Customers.Domain.Events;

public class CustomerDeletedEvent : DomainEvent
{
    public Guid Id { get; set; }
}