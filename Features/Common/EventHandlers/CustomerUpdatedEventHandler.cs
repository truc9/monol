using Features.Customers.Domain.Events;

namespace Features.Common.EventHandlers;

public class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    public Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Customer {0} Updated", notification.Id);
        return Task.CompletedTask;
    }
}