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
        NotFoundException.ThrowIfNotFound(institution, $"Institution with ID {id} not found.");
        return institution.ToDomain();
    }
    
    public async Task<Institution> UpdateInstitutionAsync(Institution institution, CancellationToken cancellationToken)
    {
        var dbInstitution = await dbContext.Institutions.FindAsync([institution.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbInstitution, $"Institution with ID {institution.Id} not found.");
        
        dbInstitution.Name = institution.Name;
        dbInstitution.ModifiedAt = DateTime.UtcNow;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"Institution with ID {institution.Id} was modified by another process.", ex);
        }
        
        return dbInstitution.ToDomain();
    }
    
    public async Task DeleteInstitutionAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbInstitution = await dbContext.Institutions.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbInstitution, $"Institution with ID {id} not found.");
        
        dbContext.Institutions.Remove(dbInstitution);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}