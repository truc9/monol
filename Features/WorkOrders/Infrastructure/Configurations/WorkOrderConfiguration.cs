using Features.WorkOrders.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Features.WorkOrders.Infrastructure.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.OwnsOne(x => x.Address);
    }
}