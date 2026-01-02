using SimpleFinance.Repository.Mappings.Accounts;
using DbAccountType = SimpleFinance.Database.Entities.AccountType;
using DomainAccountType = SimpleFinance.Domain.DomainObjects.AccountType;

namespace SimpleFinance.Repository.Tests.Mappings.Accounts;

public class AccountTypeMappingsTests
{
    #region ToDb Tests

    [Fact]
    public void ToDb_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var domain = new DomainAccountType(id, "Test Account Type");

        var result = domain.ToDb();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDb_ShouldMapNameCorrectly()
    {
        const string name = "Savings";
        var domain = new DomainAccountType(1, name);

        var result = domain.ToDb();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDb_ShouldMapDescriptionCorrectly()
    {
        const string description = "A savings account type";
        var domain = new DomainAccountType(1, "Savings", description);

        var result = domain.ToDb();

        Assert.Equal(description, result.Description);
    }

    [Fact]
    public void ToDb_WithNullDescription_ShouldMapNullCorrectly()
    {
        var domain = new DomainAccountType(1, "Savings");

        var result = domain.ToDb();

        Assert.Null(result.Description);
    }

    [Fact]
    public void ToDb_ShouldReturnDbAccountTypeInstance()
    {
        var domain = new DomainAccountType(1, "Test");

        var result = domain.ToDb();

        Assert.IsType<DbAccountType>(result);
    }

    #endregion

    #region ToDomain Tests

    [Fact]
    public void ToDomain_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var db = new DbAccountType { Id = id, Name = "Test Account Type" };

        var result = db.ToDomain();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDomain_ShouldMapNameCorrectly()
    {
        const string name = "Checking";
        var db = new DbAccountType { Id = 1, Name = name };

        var result = db.ToDomain();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDomain_ShouldMapDescriptionCorrectly()
    {
        const string description = "A checking account type";
        var db = new DbAccountType { Id = 1, Name = "Checking", Description = description };

        var result = db.ToDomain();

        Assert.Equal(description, result.Description);
    }

    [Fact]
    public void ToDomain_WithNullDescription_ShouldMapNullCorrectly()
    {
        var db = new DbAccountType { Id = 1, Name = "Checking", Description = null };

        var result = db.ToDomain();

        Assert.Null(result.Description);
    }

    [Fact]
    public void ToDomain_ShouldReturnDomainAccountTypeInstance()
    {
        var db = new DbAccountType { Id = 1, Name = "Test" };

        var result = db.ToDomain();

        Assert.IsType<DomainAccountType>(result);
    }

    #endregion

    #region Round-trip Tests

    [Fact]
    public void RoundTrip_ToDomainThenToDb_ShouldPreserveValues()
    {
        var originalDb = new DbAccountType { Id = 5, Name = "Investment", Description = "Investment accounts" };

        var domain = originalDb.ToDomain();
        var resultDb = domain.ToDb();

        Assert.Equal(originalDb.Id, resultDb.Id);
        Assert.Equal(originalDb.Name, resultDb.Name);
        Assert.Equal(originalDb.Description, resultDb.Description);
    }

    [Fact]
    public void RoundTrip_ToDbThenToDomain_ShouldPreserveValues()
    {
        var originalDomain = new DomainAccountType(10, "Credit", "Credit card accounts");

        var db = originalDomain.ToDb();
        var resultDomain = db.ToDomain();

        Assert.Equal(originalDomain.Id, resultDomain.Id);
        Assert.Equal(originalDomain.Name, resultDomain.Name);
        Assert.Equal(originalDomain.Description, resultDomain.Description);
    }

    #endregion
}

