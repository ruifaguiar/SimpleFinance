namespace SimpleFinance.Repository.Mappings.Institution;

using DbInstitution = SimpleFinance.Database.Entities.Institution;
using DomainInstitution = SimpleFinance.Domain.DomainObjects.Institution;

public static class InstitutionMappings
{
    public static DbInstitution ToDb(this DomainInstitution institution)
    {
        return new DbInstitution
        {
            Id = institution.Id,
            Name = institution.Name
        };
    }
    
    public static DomainInstitution ToDomain(this DbInstitution institution)
    {
        return new DomainInstitution(institution.Id, institution.Name);
    }
}