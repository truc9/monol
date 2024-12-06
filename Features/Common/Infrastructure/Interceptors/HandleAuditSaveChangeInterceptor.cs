using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Features.Common.Infrastructure.Interceptors;

public class HandleAuditSaveChangeInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is Entity auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedDate = DateTime.UtcNow;
                    //TODO: get from httpcontext
                    auditable.CreatedBy = "_test";
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditable.UpdatedDate = DateTime.UtcNow;
                    //TODO: get from httpcontext
                    auditable.UpdatedBy = "_test";
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}