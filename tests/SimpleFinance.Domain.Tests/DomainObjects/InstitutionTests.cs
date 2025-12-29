using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Domain.Tests.DomainObjects;

public class InstitutionTests
{
    [Fact]
    public void Constructor_WithValidId_ShouldSetId()
    {
        var id = Guid.NewGuid();
        
        var institution = new Institution(id, "Test");
        
        Assert.Equal(id, institution.Id);
    }

    [Fact]
    public void Constructor_WithValidName_ShouldSetName()
    {
        const string name = "Test Institution";
        
        var institution = new Institution(Guid.NewGuid(), name);
        
        Assert.Equal(name, institution.Name);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(Guid.Empty, "Test"));
        
        Assert.Equal("id", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ShouldContainCorrectMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(Guid.Empty, "Test"));
        
        Assert.Contains("Id cannot be empty", exception.Message);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(Guid.NewGuid(), string.Empty));
        
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldContainCorrectMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(Guid.NewGuid(), string.Empty));
        
        Assert.Contains("Name cannot be empty", exception.Message);
    }

    [Fact]
    public void Equality_TwoInstitutionsWithSameValues_ShouldBeEqual()
    {
        var id = Guid.NewGuid();
        const string name = "Test";
        
        var institution1 = new Institution(id, name);
        var institution2 = new Institution(id, name);
        
        Assert.Equal(institution1, institution2);
    }

    [Fact]
    public void Equality_TwoInstitutionsWithDifferentIds_ShouldNotBeEqual()
    {
        var institution1 = new Institution(Guid.NewGuid(), "Test");
        var institution2 = new Institution(Guid.NewGuid(), "Test");
        
        Assert.NotEqual(institution1, institution2);
    }

    [Fact]
    public void Equality_TwoInstitutionsWithDifferentNames_ShouldNotBeEqual()
    {
        var id = Guid.NewGuid();
        
        var institution1 = new Institution(id, "Test1");
        var institution2 = new Institution(id, "Test2");
        
        Assert.NotEqual(institution1, institution2);
    }
}