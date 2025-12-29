using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Mappings.Institution;

public static class InstitutionMappings
{
    public static Domain.DomainObjects.Institution ToDomain(this InstitutionModel institutionModel)
    {
        institutionModel.Id ??= Guid.NewGuid();
            
        return new Domain.DomainObjects.Institution(institutionModel.Id.Value, institutionModel.Name);
    }
    
    public static InstitutionModel ToModel(this Domain.DomainObjects.Institution institution)
    {
        return new InstitutionModel
        {
            Id = institution.Id,
            Name = institution.Name
        };
    }
}