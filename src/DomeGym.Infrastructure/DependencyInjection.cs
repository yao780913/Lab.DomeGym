using DomeGym.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, string? connectionString = null)
    {
        services
            .AddServices()
            .AddPersistence(connectionString ?? "Data Source = DomeGym.db");

        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
    
    private static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DomeGymDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        return services;
    }
}