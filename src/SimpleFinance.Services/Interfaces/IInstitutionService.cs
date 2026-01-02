using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface IInstitutionService
{
    Task<Institution> AddInstitutionAsync(Institution institution, CancellationToken cancellationToken);
    Task<IEnumerable<Institution>> GetAllInstitutionsAsync(CancellationToken cancellationToken);
    Task<Institution> GetInstitutionByIdAsync(int id, CancellationToken cancellationToken);
    Task<Institution> UpdateInstitutionAsync(Institution institution, CancellationToken cancellationToken);
    Task DeleteInstitutionAsync(int id, CancellationToken cancellationToken);
}