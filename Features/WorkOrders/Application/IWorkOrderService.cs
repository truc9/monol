using Features.WorkOrders.Application.Models;

namespace Features.WorkOrders.Application;

public interface IWorkOrderService
{
    Task<Guid> CreateAsync(WorkOrderModel model);
    Task<PagingResult<WorkOrderDetailsModel>> GetPagingAsync(int pageNumber, int pageSize);
}