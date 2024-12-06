using Features.Customers.Application.Models;
using Features.Customers.Application.Services;

namespace Application.IntegrationTest;

public class CustomerServiceTest(InfrastructureFixture fixture) : IClassFixture<InfrastructureFixture>
{
    [Fact]
    public async Task CustomerService_Create_ShouldSuccess()
    {
        var service = new CustomerService(fixture.TestDbContext);
        var customerId = await service.CreateAsync(new CustomerOverviewModel
        {
            FirstName = "Truc",
            LastName = "Nguyen",
            Email = "truc.nguyen@monol.co.uk",
            Phone = "0123456789",
        });

        Assert.IsType<Guid>(customerId);
        var created = await fixture.TestDbContext.Customers.FindAsync(customerId);
        Assert.NotNull(created);
        Assert.Equal("Truc", created.FirstName);
        Assert.Equal("Nguyen", created.LastName);
        Assert.Equal("truc.nguyen@monol.co.uk", created.Email);
        Assert.Equal("0123456789", created.Phone);
    }

    [Fact]
    public async Task CustomerService_GetPaging_ShouldReturnOneRecordInPagingResult()
    {
        var service = new CustomerService(fixture.TestDbContext);
        var customerId = await service.CreateAsync(new CustomerOverviewModel
        {
            FirstName = "Truc",
            LastName = "Nguyen",
            Email = "truc.nguyen@monol.co.uk",
            Phone = "0123456789",
        });

        var pagingResult = await service.GetPagingAsync(0, 25);
        Assert.NotNull(pagingResult);
        Assert.Equal(1, pagingResult.Total);
        Assert.Equal(0, pagingResult.PageNumber);
        Assert.Equal(25, pagingResult.PageSize);
        Assert.Equal("Truc", pagingResult.Data[0].FirstName);
        Assert.Equal("Nguyen", pagingResult.Data[0].LastName);
        Assert.Equal("truc.nguyen@monol.co.uk", pagingResult.Data[0].Email);
        Assert.Equal("0123456789", pagingResult.Data[0].Phone);
    }
}