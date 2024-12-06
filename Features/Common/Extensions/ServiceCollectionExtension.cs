using Features.Common.Infrastructure.Interceptors;
using Features.Customers.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Features.Common.Extensions;

public class F;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(F).Assembly));
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }

    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<HandleAuditSaveChangeInterceptor>();
        services.AddScoped<HandleDomainEventSaveChangeInterceptor>();
        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(connectionString, m => { m.MigrationsAssembly("Migrations"); });
            opt.AddInterceptors(
                sp.GetRequiredService<HandleAuditSaveChangeInterceptor>(),
                sp.GetRequiredService<HandleDomainEventSaveChangeInterceptor>()
            );
        });

        return services;
    }

    public static IServiceCollection AddSQLServer(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<HandleAuditSaveChangeInterceptor>();
        services.AddScoped<HandleDomainEventSaveChangeInterceptor>();
        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(connectionString, m => { m.MigrationsAssembly("Migrations"); });
            opt.AddInterceptors(
                sp.GetRequiredService<HandleAuditSaveChangeInterceptor>(),
                sp.GetRequiredService<HandleDomainEventSaveChangeInterceptor>()
            );
        });

        return services;
    }
}