using Features.WorkOrders.Application;
using Features.WorkOrders.Application.Models;

namespace API.Endpoints;

public static class WorkOrders
{
    public static WebApplication UseWorkOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/work-orders");

        group.MapGet("/",
            async ([FromQuery] int pageNumber, [FromQuery] int pageSize,
                [FromServices] IWorkOrderService workOrderService) =>
            {
                var result = await workOrderService.GetPagingAsync(pageNumber, pageSize);
                return Results.Ok(result);
            });

        group.MapPost("/", async ([FromBody] WorkOrderModel model, [FromServices] IWorkOrderService workOrderService) =>
        {
            var result = await workOrderService.CreateAsync(model);
            return Results.Ok(result);
        });
        
        return app;
    }
}