using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database.Entities;

namespace SimpleFinance.Database;

public class SimpleFinanceDbContext(DbContextOptions<SimpleFinanceDbContext> options) : DbContext(options)
{
    public DbSet<Institution> Institutions { get; set; }
}