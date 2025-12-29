using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleFinance.Database;

/// <summary>
/// Factory for creating DbContext instances at design time (e.g., for migrations).
/// </summary>
public class SimpleFinanceDbContextFactory : IDesignTimeDbContextFactory<SimpleFinanceDbContext>
{
    public SimpleFinanceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SimpleFinanceDbContext>();
        
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") 
            ?? "Host=localhost;Database=simplefinance;Username=postgres;Password=postgres";
        
        optionsBuilder.UseNpgsql(connectionString);

        return new SimpleFinanceDbContext(optionsBuilder.Options);
    }
}

