using API.Requests;
using FastEndpoints;
using Features.Customers.Application.Models;
using Features.Customers.Application.Services;

namespace API.Endpoints;

public class GetPaging(ICustomerService customerService) : Endpoint<PagingRequest, PagingResult<CustomerDetailsModel>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("v1/customers");
    }

    public override async Task HandleAsync(PagingRequest req, CancellationToken ct)
    {
        Response = await customerService.GetPagingAsync(req.PageNumber, req.PageSize, ct);
    }
}

public class GetCustomerById(ICustomerService customerService) : EndpointWithoutRequest<CustomerDetailsModel>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("v1/customers/{customerId:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = Route<Guid>("customerId", isRequired: true);
        Response = await customerService.GetAsync(customerId, ct);
    }
}

public class Create(ILogger<Create> logger, ICustomerService customerService)
    : Endpoint<CustomerOverviewModel, Guid>
{
    public override void Configure()
    {
        AllowAnonymous();
        logger.LogInformation("Initializing Endpoint");
        Post("v1/customers");
    }

    public override async Task HandleAsync(CustomerOverviewModel req, CancellationToken ct)
    {
        logger.LogInformation("Create Customer Handled");
        Response = await customerService.CreateAsync(req, ct);
    }
}

public class Update(ICustomerService customerService) : Endpoint<CustomerOverviewModel>
{
    public override void Configure()
    {
        AllowAnonymous();
        Put("v1/customers/{customerId:guid}");
    }

    public override async Task HandleAsync(CustomerOverviewModel req, CancellationToken ct)
    {
        var customerId = Route<Guid>("customerId", isRequired: true);
        await customerService.UpdateAsync(customerId, req, ct);
    }
}

public class Delete(ICustomerService customerService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        AllowAnonymous();
        Delete("v1/customers/{customerId:guid}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customerId = Route<Guid>("customerId", isRequired: true);
        await customerService.DeleteAsync(customerId, ct);
    }
}