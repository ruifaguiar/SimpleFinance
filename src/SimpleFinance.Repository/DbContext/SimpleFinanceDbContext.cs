namespace SimpleFinance.Repository.DbContext;

using Microsoft.EntityFrameworkCore;

public class SimpleFinanceDbContext : DbContext
{
    public SimpleFinanceDbContext(DbContextOptions<SimpleFinanceDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuration is done via dependency injection
    }
}