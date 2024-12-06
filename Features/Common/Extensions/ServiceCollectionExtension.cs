using Features.Common.Infrastructure.Interceptors;
using Features.Customers.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Features.Common.Extensions;

interface IFeatureMarker;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(IFeatureMarker).Assembly));
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }

    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(connectionString, m => { m.MigrationsAssembly("Migrations"); });
            opt.AddInterceptors(
                sp.GetRequiredService<AuditSaveChangeInterceptor>(),
                sp.GetRequiredService<PublishEventSaveChangeInterceptor>()
            );
        });

        return services;
    }

    public static IServiceCollection AddSQLServer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(connectionString, m => { m.MigrationsAssembly("Migrations"); });
            opt.AddInterceptors(
                sp.GetRequiredService<AuditSaveChangeInterceptor>(),
                sp.GetRequiredService<PublishEventSaveChangeInterceptor>()
            );
        });

        return services;
    }
}