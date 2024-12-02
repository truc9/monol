using Features.WorkOrders.Application.Models;
using Features.WorkOrders.Domain;

namespace Features.WorkOrders.Application;

public class WorkOrderService(AppDbContext db) : IWorkOrderService
{
    public async Task<Guid> CreateAsync(WorkOrderModel model)
    {
        var wo = new WorkOrder
        {
            Reference = model.Reference,
            Description = model.Description,
            Address = model.Address,
            PlanEndDate = model.PlanEndDate,
            PlanStartDate = model.PlanStartDate,
        };
        db.WorkOrders.Add(wo);
        await db.SaveChangesAsync();
        return wo.Id;
    }

    public async Task<PagingResult<WorkOrderDetailsModel>> GetPagingAsync(int pageNumber, int pageSize)
    {
        var query = db.WorkOrders.Select(x => new WorkOrderDetailsModel
        {
            Id = x.Id,
            Address = $"{x.Address.Street} {x.Address.City} {x.Address.Postcode}, {x.Address.Country}",
            Description = x.Description,
            Reference = x.Reference,
            PlanEndDate = x.PlanEndDate,
            PlanStartDate = x.PlanStartDate,
            ActualEndDate = x.ActualEndDate,
            ActualStartDate = x.ActualStartDate,
        });
        return await query.ToPagingAsync(pageNumber, pageSize);
    }
}