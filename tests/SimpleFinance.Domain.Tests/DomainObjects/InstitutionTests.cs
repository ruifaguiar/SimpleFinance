using SimpleFinance.Domain.DomainObjects;

namespace SimpleFinance.Domain.Tests.DomainObjects;

public class InstitutionTests
{
    [Fact]
    public void Constructor_WithValidId_ShouldSetId()
    {
        const int id = 2;
        
        var institution = new Institution(id, "Test");
        
        Assert.Equal(id, institution.Id);
    }

    [Fact]
    public void Constructor_WithValidName_ShouldSetName()
    {
        const string name = "Test Institution";
        
        var institution = new Institution(1, name);
        
        Assert.Equal(name, institution.Name);
    }

    [Fact]
    public void Constructor_WithNegativeId_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(-1, "Test"));
        
        Assert.Equal("id", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNegativeId_ShouldContainCorrectMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(-1, "Test"));
        
        Assert.Contains("Id cannot be less than 0", exception.Message);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(2, string.Empty));
        
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldContainCorrectMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Institution(1, string.Empty));
        
        Assert.Contains("Name cannot be empty", exception.Message);
    }

    [Fact]
    public void Equality_TwoInstitutionsWithSameId_ShouldBeEqual()
    {
        const int id = 1;
        
        var institution1 = new Institution(id, "Name1");
        var institution2 = new Institution(id, "Name2");
        
        Assert.Equal(institution1, institution2);
    }

    [Fact]
    public void Equality_TwoInstitutionsWithDifferentIds_ShouldNotBeEqual()
    {
        var institution1 = new Institution(1, "Test");
        var institution2 = new Institution(2, "Test");
        
        Assert.NotEqual(institution1, institution2);
    }

    [Fact]
    public void GetHashCode_TwoInstitutionsWithSameId_ShouldHaveSameHashCode()
    {
        const int id = 1;
        
        var institution1 = new Institution(id, "Name1");
        var institution2 = new Institution(id, "Name2");
        
        Assert.Equal(institution1.GetHashCode(), institution2.GetHashCode());
    }
    
}