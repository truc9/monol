using Features.Customers.Application.Services;
using Features.WorkOrders.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Features.Common.Extensions;

interface IFeatureMarker;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(IFeatureMarker).Assembly));
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IWorkOrderService, WorkOrderService>();
        return services;
    }

    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString, m => { m.MigrationsAssembly("Migrations"); });
        });

        return services;
    }
}