using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Implementation;

public class InstitutionService(IInstitutionRepository repository) : IInstitutionService
{
    public Task<Institution> AddInstitutionAsync(Institution institution)
    {
        return repository.AddInstitutionAsync(institution);
    }
    
    public Task<IEnumerable<Institution>> GetAllInstitutionsAsync()
    {
        return repository.GetAllInstitutionsAsync();
    }
}