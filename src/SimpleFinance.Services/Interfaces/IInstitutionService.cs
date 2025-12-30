using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface IInstitutionService
{
    Task<Institution> AddInstitutionAsync(Institution institution);
    Task<IEnumerable<Institution>> GetAllInstitutionsAsync();
}