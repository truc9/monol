using Features.Customers.Domain.Events;

namespace Features.Common.EventHandlers;

public class CustomerDeletedEventHandler : INotificationHandler<CustomerDeletedEvent>
{
    public Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Customer {0} Deleted", notification.Id);
        return Task.CompletedTask;
    }
}