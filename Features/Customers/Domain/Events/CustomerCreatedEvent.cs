namespace Features.Customers.Domain.Events;

public class CustomerCreatedEvent : DomainEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}