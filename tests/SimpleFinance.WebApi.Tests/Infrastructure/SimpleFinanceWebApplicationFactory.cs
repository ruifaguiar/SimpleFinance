using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleFinance.Database;

namespace SimpleFinance.WebApi.Tests.Infrastructure;

public class SimpleFinanceWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = "InMemoryDbForTesting_" + Guid.NewGuid();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove all DbContext related registrations
            var descriptorsToRemove = services.Where(
                d => d.ServiceType == typeof(DbContextOptions<SimpleFinanceDbContext>) ||
                     d.ServiceType == typeof(SimpleFinanceDbContext) ||
                     d.ServiceType.FullName?.Contains("EntityFrameworkCore") == true).ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Add an in-memory database for testing
            services.AddDbContext<SimpleFinanceDbContext>(options =>
            {
                options.UseInMemoryDatabase(_databaseName);
            });
        });

        builder.UseEnvironment("Development");
    }
}

