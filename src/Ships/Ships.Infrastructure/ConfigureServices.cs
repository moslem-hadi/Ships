using Ships.Application.Common.Interfaces;
using Ships.Infrastructure.Persistence;
using Ships.Infrastructure.Persistence.Interceptors;
using Ships.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ShipsDb"));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
    

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
