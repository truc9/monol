using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Features.Common.Infrastructure.Interceptors;

public class AuditSaveChangeInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context!;
        foreach (var entry in dbContext.ChangeTracker.Entries()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
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

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}