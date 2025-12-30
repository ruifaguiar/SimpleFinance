using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database.Entities;

namespace SimpleFinance.Database;

public class SimpleFinanceDbContext(DbContextOptions<SimpleFinanceDbContext> options) : DbContext(options)
{
    public DbSet<Institution> Institutions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>()
            .Property<uint>("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}