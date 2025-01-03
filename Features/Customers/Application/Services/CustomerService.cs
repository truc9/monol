using Features.Customers.Application.Models;
using Features.Customers.Domain;
using Features.Customers.Domain.Events;

namespace Features.Customers.Application.Services;

public class CustomerService(AppDbContext db) : ICustomerService
{
    public async Task<Guid> CreateAsync(CustomerOverviewModel overviewModel,
        CancellationToken ct = default)
    {
        var customer = new Customer
        {
            FirstName = overviewModel.FirstName,
            LastName = overviewModel.LastName,
            Email = overviewModel.Email,
            Phone = overviewModel.Phone,
        };

        customer.AddEvent(new CustomerCreatedEvent
        {
            Id = customer.Id,
            Name = $"{customer.FirstName} {customer.LastName}"
        });

        db.Customers.Add(customer);
        await db.SaveChangesAsync(ct);
        return customer.Id;
    }

    public async Task UpdateAsync(Guid id, CustomerOverviewModel overviewModel,
        CancellationToken ct = default)
    {
        var customer = await db.Customers.FindAsync(id, ct);
        if (customer is null) throw new DomainException($"Customer {id} not found");

        customer.AddEvent(new CustomerUpdatedEvent
        {
            Id = customer.Id,
            OldName = $"{customer.FirstName} {customer.LastName}",
            NewName = $"{overviewModel.FirstName} {overviewModel.LastName}",
        });

        customer.FirstName = overviewModel.FirstName;
        customer.LastName = overviewModel.LastName;
        customer.Email = overviewModel.Email;
        customer.Phone = overviewModel.Phone;
        
        await db.SaveChangesAsync(ct);
    }

    public async Task<PagingResult<CustomerDetailsModel>> GetPagingAsync(int pageNumber, int pageSize,
        CancellationToken ct = default)
    {
        var query = db.Customers.Select(c => new CustomerDetailsModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Phone = c.Phone,
        });

        return await query.ToPagingAsync(pageNumber, pageSize, ct);
    }

    public async Task<CustomerDetailsModel?> GetAsync(Guid customerId, CancellationToken ct = default)
    {
        return await db.Customers
            .Where(c => c.Id == customerId)
            .Select(c => new CustomerDetailsModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task DeleteAsync(Guid customerId, CancellationToken ct = default)
    {
        var customer = await db.Customers.FindAsync(customerId, ct);
        if (customer is null) throw new DomainException($"Customer {customerId} not found");
        db.Customers.Remove(customer);
        await db.SaveChangesAsync(ct);
        customer.AddEvent(new CustomerDeletedEvent { Id = customer.Id });
    }
}