using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleFinance.Database.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SimpleFinanceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection") 
                              ?? "Host=localhost;Database=simplefinance;Username=postgres;Password=postgres"));
        return services;
    }
}