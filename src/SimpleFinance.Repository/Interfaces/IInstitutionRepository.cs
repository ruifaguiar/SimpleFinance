using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IInstitutionRepository
{
    Task<Institution> AddInstitutionAsync(Institution institution);
    Task<IEnumerable<Institution>> GetAllInstitutionsAsync();
}