using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IInstitutionRepository
{
    Task<Institution> AddInstitutionAsync(Institution institution, CancellationToken cancellationToken);
    Task<IEnumerable<Institution>> GetAllInstitutionsAsync(CancellationToken cancellationToken);
    Task<Institution> GetInstitutionByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Institution> UpdateInstitutionAsync(Institution institution, CancellationToken cancellationToken);
}