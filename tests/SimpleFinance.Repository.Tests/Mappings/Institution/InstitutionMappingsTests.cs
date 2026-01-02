using SimpleFinance.Repository.Mappings.Institution;
using DbInstitution = SimpleFinance.Database.Entities.Institution;
using DomainInstitution = SimpleFinance.Domain.DomainObjects.Institution;

namespace SimpleFinance.Repository.Tests.Mappings.Institution;

public class InstitutionMappingsTests
{
    [Fact]
    public void ToDb_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var domain = new DomainInstitution(id, "Test Institution");

        var result = domain.ToDb();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDb_ShouldMapNameCorrectly()
    {
        const string name = "Test Institution";
        var domain = new DomainInstitution(1, name);

        var result = domain.ToDb();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDb_ShouldReturnDbInstitutionType()
    {
        var domain = new DomainInstitution(1, "Test");

        var result = domain.ToDb();

        Assert.IsType<DbInstitution>(result);
    }

    [Fact]
    public void ToDomain_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var db = new DbInstitution { Id = id, Name = "Test Institution" };

        var result = db.ToDomain();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDomain_ShouldMapNameCorrectly()
    {
        const string name = "Test Institution";
        var db = new DbInstitution { Id = 1, Name = name };

        var result = db.ToDomain();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDomain_ShouldReturnDomainInstitutionType()
    {
        var db = new DbInstitution { Id = 1, Name = "Test" };

        var result = db.ToDomain();

        Assert.IsType<DomainInstitution>(result);
    }
}