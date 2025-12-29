using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Repository.Interfaces;

public interface IInstitutionRepository
{
    public Task<Institution> AddInstitutionAsync(Institution institution);
}