using SimpleFinance.Repository.Mappings.Institution;
using DbInstitution = SimpleFinance.Database.Entities.Institution;
using DomainInstitution = SimpleFinance.Domain.DomainObjects.Institution;

namespace SimpleFinance.Repository.Tests.Mappings.Institution;

public class InstitutionMappingsTests
{
    [Fact]
    public void ToDb_ShouldMapIdCorrectly()
    {
        var id = Guid.NewGuid();
        var domain = new DomainInstitution(id, "Test Institution");

        var result = domain.ToDb();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDb_ShouldMapNameCorrectly()
    {
        var name = "Test Institution";
        var domain = new DomainInstitution(Guid.NewGuid(), name);

        var result = domain.ToDb();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDb_ShouldReturnDbInstitutionType()
    {
        var domain = new DomainInstitution(Guid.NewGuid(), "Test");

        var result = domain.ToDb();

        Assert.IsType<DbInstitution>(result);
    }

    [Fact]
    public void ToDb_WithEmptyName_ShouldMapCorrectly()
    {
        var domain = new DomainInstitution(Guid.NewGuid(), string.Empty);

        var result = domain.ToDb();

        Assert.Equal(string.Empty, result.Name);
    }
}