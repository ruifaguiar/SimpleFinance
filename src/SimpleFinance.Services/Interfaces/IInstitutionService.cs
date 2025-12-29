using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Services.Interfaces;

public interface IInstitutionService
{
    public Task<Institution> AddInstitutionAsync(Institution institution);
}