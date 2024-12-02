using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Share;

public static class Extensions
{
    public static async Task PublishEventAsync(this IMediator mediator, DbContext db, CancellationToken cts = default)
    {
        var query = db.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events.Any());

        if (query.Any())
        {
            var events = query.SelectMany(x => x.Entity.Events).ToList();

            if (events.Any())
            {
                foreach (var @event in events)
                {
                    await mediator.Publish(@event, cts);
                }
            }

            query.ToList().ForEach(e => e.Entity.ClearEvents());    
        }
    }
}