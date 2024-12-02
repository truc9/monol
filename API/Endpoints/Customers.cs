using Features.Customers.Application.Models;
using Features.Customers.Application.Services;

namespace API.Endpoints;

public static class Customers
{
    public static WebApplication UseCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/customers");

        group.MapGet("/",
            async ([FromQuery] int pageNumber, [FromQuery] int pageSize,
                [FromServices] ICustomerService customerService) =>
            {
                var result = await customerService.GetPagingAsync(pageNumber, pageSize);
                return Results.Ok(result);
            });

        group.MapPost("/", async ([FromBody] CustomerModel dto, [FromServices] ICustomerService customerService) =>
        {
            var result = await customerService.CreateAsync(dto);
            return Results.Ok(result);
        });

        group.MapPut("/{customerId:guid}",
            async ([FromBody] CustomerModel dto, [FromServices] ICustomerService customerService) =>
            {
                var result = await customerService.CreateAsync(dto);
                return Results.Ok(result);
            });

        return app;
    }
}