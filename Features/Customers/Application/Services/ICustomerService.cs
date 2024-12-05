using Features.Customers.Application.Models;

namespace Features.Customers.Application.Services;

public interface ICustomerService
{
    Task<Guid> CreateAsync(CustomerOverviewModel overviewModel, CancellationToken ct = default);
    Task UpdateAsync(Guid id, CustomerOverviewModel overviewModel, CancellationToken ct = default);

    Task<PagingResult<CustomerDetailsModel>> GetPagingAsync(int pageNumber, int pageSize,
        CancellationToken ct = default);

    Task<CustomerDetailsModel?> GetAsync(Guid customerId, CancellationToken ct = default);
    Task DeleteAsync(Guid customerId, CancellationToken ct = default);
}