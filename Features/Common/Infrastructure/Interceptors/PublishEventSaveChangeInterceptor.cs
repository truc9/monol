using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Features.Common.Infrastructure.Interceptors;

public class PublishEventSaveChangeInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await base.SavedChangesAsync(eventData, result, cancellationToken);
        await mediator.PublishEventAsync(eventData.Context!, cancellationToken);
        return response;
    }
}