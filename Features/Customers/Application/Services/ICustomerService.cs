using Features.Customers.Application.Models;

namespace Features.Customers.Application.Services;

public interface ICustomerService
{
    Task<Guid> CreateAsync(CustomerModel model);
    Task UpdateAsync(Guid id, CustomerModel model);
    Task<PagingResult<CustomerDetailsModel>> GetPagingAsync(int pageNumber, int pageSize);
}