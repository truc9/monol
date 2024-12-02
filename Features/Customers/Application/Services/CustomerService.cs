using Features.Customers.Application.Models;
using Features.Customers.Domain;

namespace Features.Customers.Application.Services;

public class CustomerService(GridwiseDbContext db) : ICustomerService
{
    public async Task<Guid> CreateAsync(CustomerModel model)
    {
        var customer = new Customer
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
        };
        db.Customers.Add(customer);
        await db.SaveChangesAsync();
        return customer.Id;
    }

    public async Task UpdateAsync(Guid id, CustomerModel model)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer is null) throw new DomainException($"Customer {id} not found");

        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.Email = model.Email;
        customer.Phone = model.Phone;
        await db.SaveChangesAsync();
    }

    public Task<PagingResult<CustomerDetailsModel>> GetPagingAsync(int pageNumber, int pageSize)
    {
        var query = db.Customers.Select(c => new CustomerDetailsModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Phone = c.Phone,
        });

        return query.ToPagingAsync(pageNumber, pageSize);
    }
}