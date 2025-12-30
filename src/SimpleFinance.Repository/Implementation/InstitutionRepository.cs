using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.Institution;

namespace SimpleFinance.Repository.Implementation;

public class InstitutionRepository(SimpleFinanceDbContext dbContext) : IInstitutionRepository
{
    public async Task<Institution> AddInstitutionAsync(Institution institution)
    {
        var dbInstitution = institution.ToDb();
        dbInstitution.CreatedAt = DateTime.UtcNow;

        dbContext.Institutions.Add(dbInstitution);
        await dbContext.SaveChangesAsync();
        return institution;
    }
    
    public async Task<IEnumerable<Institution>> GetAllInstitutionsAsync()
    {
        var institutions = await dbContext.Institutions.ToListAsync();
        return institutions.Select(i => i.ToDomain());
    }
}