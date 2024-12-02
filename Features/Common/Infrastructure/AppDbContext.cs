using Features.Customers.Domain;
using Features.WorkOrders.Domain;
using MediatR;

namespace Features.Common.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<WorkOrderTask> WorkOrderTasks { get; set; }
    public DbSet<WorkOrderTaskType> WorkOrderTaskTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await mediator.PublishEventAsync(this, cancellationToken);
        return result;
    }
}