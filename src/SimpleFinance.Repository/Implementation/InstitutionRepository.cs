using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.Institution;

namespace SimpleFinance.Repository.Implementation;

public class InstitutionRepository(SimpleFinanceDbContext dbContext) : IInstitutionRepository
{
    public async Task<Institution> AddInstitutionAsync(Institution institution, CancellationToken cancellationToken)
    {
        var dbInstitution = institution.ToDb();
        dbInstitution.CreatedAt = DateTime.UtcNow;

        dbContext.Institutions.Add(dbInstitution);
        await dbContext.SaveChangesAsync(cancellationToken);
        return institution;
    }
    
    public async Task<IEnumerable<Institution>> GetAllInstitutionsAsync(CancellationToken cancellationToken)
    {
        var institutions = await dbContext.Institutions.ToListAsync(cancellationToken);
        return institutions.Select(i => i.ToDomain());
    }
    
    public async Task<Institution> GetInstitutionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var institution = await dbContext.Institutions.FindAsync([id], cancellationToken);
        InstitutionNotFoundException.ThrowIfNotFound(institution,id);
        return institution.ToDomain();
    }
    
    public async Task<Institution> UpdateInstitutionAsync(Institution institution, CancellationToken cancellationToken)
    {
        var dbInstitution = await dbContext.Institutions.FindAsync([institution.Id], cancellationToken);
        InstitutionNotFoundException.ThrowIfNotFound(dbInstitution,institution.Id);
        
        dbInstitution.Name = institution.Name;
        dbInstitution.ModifiedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return dbInstitution.ToDomain();
    }
}