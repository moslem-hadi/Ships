using Ships.Application.Common.Interfaces;
using Ships.Infrastructure.Persistence;
using Ships.Infrastructure.Persistence.Interceptors;
using Ships.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    private const string databaseName = "ShipsDb";
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseInMemoryDatabase(databaseName));

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
    

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
