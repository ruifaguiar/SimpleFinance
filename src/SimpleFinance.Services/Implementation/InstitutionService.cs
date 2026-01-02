using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation.Accounts;

public class InstitutionService(IInstitutionRepository repository) : IInstitutionService
{
    public Task<Institution> AddInstitutionAsync(Institution institution, CancellationToken cancellationToken)
    {
        return repository.AddInstitutionAsync(institution, cancellationToken);
    }
    
    public Task<IEnumerable<Institution>> GetAllInstitutionsAsync(CancellationToken cancellationToken)
    {
        return repository.GetAllInstitutionsAsync(cancellationToken);
    }
    
    public Task<Institution> GetInstitutionByIdAsync(int id, CancellationToken cancellationToken)
    {
        return repository.GetInstitutionByIdAsync(id, cancellationToken);
    }
    
    public Task<Institution> UpdateInstitutionAsync(Institution institution, CancellationToken cancellationToken)
    {
        return repository.UpdateInstitutionAsync(institution, cancellationToken);
    }
    
    public Task DeleteInstitutionAsync(int id, CancellationToken cancellationToken)
    {
        return repository.DeleteInstitutionAsync(id, cancellationToken);
    }
}