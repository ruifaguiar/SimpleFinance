using SimpleFinance.Repository.Mappings.Accounts;
using DbAccount = SimpleFinance.Database.Entities.Account;
using DomainAccount = SimpleFinance.Domain.DomainObjects.Account;

namespace SimpleFinance.Repository.Tests.Mappings.Accounts;

public class AccountMappingsTests
{
    #region ToDb Tests

    [Fact]
    public void ToDb_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var domain = CreateValidDomainAccount(id: id);

        var result = domain.ToDb();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDb_ShouldMapNameCorrectly()
    {
        const string name = "My Savings Account";
        var domain = CreateValidDomainAccount(name: name);

        var result = domain.ToDb();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDb_ShouldMapAccountNumberCorrectly()
    {
        const string accountNumber = "123456789";
        var domain = CreateValidDomainAccount(accountNumber: accountNumber);

        var result = domain.ToDb();

        Assert.Equal(accountNumber, result.AccountNumber);
    }

    [Fact]
    public void ToDb_ShouldMapIbanCorrectly()
    {
        const string iban = "PT50000201231234567890154";
        var domain = CreateValidDomainAccount(iban: iban);

        var result = domain.ToDb();

        Assert.Equal(iban, result.Iban);
    }

    [Fact]
    public void ToDb_ShouldMapSwiftBicCorrectly()
    {
        const string swiftBic = "BPOTPTPL";
        var domain = CreateValidDomainAccount(swiftBic: swiftBic);

        var result = domain.ToDb();

        Assert.Equal(swiftBic, result.SwiftBic);
    }

    [Fact]
    public void ToDb_ShouldMapAccountTypeIdCorrectly()
    {
        const int accountTypeId = 5;
        var domain = CreateValidDomainAccount(accountTypeId: accountTypeId);

        var result = domain.ToDb();

        Assert.Equal(accountTypeId, result.AccountTypeId);
    }

    [Fact]
    public void ToDb_ShouldMapBalanceCorrectly()
    {
        const decimal balance = 1500.75m;
        var domain = CreateValidDomainAccount(balance: balance);

        var result = domain.ToDb();

        Assert.Equal(balance, result.Balance);
    }

    [Fact]
    public void ToDb_ShouldMapCurrencyCorrectly()
    {
        const string currency = "USD";
        var domain = CreateValidDomainAccount(currency: currency);

        var result = domain.ToDb();

        Assert.Equal(currency, result.Currency);
    }

    [Fact]
    public void ToDb_ShouldMapInstitutionIdCorrectly()
    {
        const int institutionId = 10;
        var domain = CreateValidDomainAccount(institutionId: institutionId);

        var result = domain.ToDb();

        Assert.Equal(institutionId, result.InstitutionId);
    }

    [Fact]
    public void ToDb_ShouldMapIsActiveCorrectly()
    {
        const bool isActive = false;
        var domain = CreateValidDomainAccount(isActive: isActive);

        var result = domain.ToDb();

        Assert.Equal(isActive, result.IsActive);
    }

    [Fact]
    public void ToDb_ShouldMapOpenedAtCorrectly()
    {
        var openedAt = new DateOnly(2024, 1, 15);
        var domain = CreateValidDomainAccount(openedAt: openedAt);

        var result = domain.ToDb();

        Assert.Equal(openedAt, result.OpenedAt);
    }

    [Fact]
    public void ToDb_ShouldMapClosedAtCorrectly()
    {
        var closedAt = new DateOnly(2025, 12, 31);
        var domain = CreateValidDomainAccount(closedAt: closedAt);

        var result = domain.ToDb();

        Assert.Equal(closedAt, result.ClosedAt);
    }

    [Fact]
    public void ToDb_ShouldMapCreatedAtCorrectly()
    {
        var createdAt = new DateTime(2024, 6, 1, 10, 30, 0, DateTimeKind.Utc);
        var domain = CreateValidDomainAccount(createdAt: createdAt);

        var result = domain.ToDb();

        Assert.Equal(createdAt, result.CreatedAt);
    }

    [Fact]
    public void ToDb_ShouldMapModifiedAtCorrectly()
    {
        var modifiedAt = new DateTime(2024, 7, 15, 14, 0, 0, DateTimeKind.Utc);
        var domain = CreateValidDomainAccount(modifiedAt: modifiedAt);

        var result = domain.ToDb();

        Assert.Equal(modifiedAt, result.ModifiedAt);
    }

    [Fact]
    public void ToDb_WithNullOptionalFields_ShouldMapNullsCorrectly()
    {
        var domain = CreateValidDomainAccount(
            accountNumber: null,
            iban: null,
            swiftBic: null,
            openedAt: null,
            closedAt: null,
            modifiedAt: null);

        var result = domain.ToDb();

        Assert.Null(result.AccountNumber);
        Assert.Null(result.Iban);
        Assert.Null(result.SwiftBic);
        Assert.Null(result.OpenedAt);
        Assert.Null(result.ClosedAt);
        Assert.Null(result.ModifiedAt);
    }

    [Fact]
    public void ToDb_ShouldReturnDbAccountInstance()
    {
        var domain = CreateValidDomainAccount();

        var result = domain.ToDb();

        Assert.IsType<DbAccount>(result);
    }

    #endregion

    #region ToDomain Tests

    [Fact]
    public void ToDomain_ShouldMapIdCorrectly()
    {
        const int id = 1;
        var db = CreateValidDbAccount(id: id);

        var result = db.ToDomain();

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void ToDomain_ShouldMapNameCorrectly()
    {
        const string name = "My Checking Account";
        var db = CreateValidDbAccount(name: name);

        var result = db.ToDomain();

        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void ToDomain_ShouldMapAccountNumberCorrectly()
    {
        const string accountNumber = "987654321";
        var db = CreateValidDbAccount(accountNumber: accountNumber);

        var result = db.ToDomain();

        Assert.Equal(accountNumber, result.AccountNumber);
    }

    [Fact]
    public void ToDomain_ShouldMapIbanCorrectly()
    {
        const string iban = "DE89370400440532013000";
        var db = CreateValidDbAccount(iban: iban);

        var result = db.ToDomain();

        Assert.Equal(iban, result.Iban);
    }

    [Fact]
    public void ToDomain_ShouldMapSwiftBicCorrectly()
    {
        const string swiftBic = "COBADEFF";
        var db = CreateValidDbAccount(swiftBic: swiftBic);

        var result = db.ToDomain();

        Assert.Equal(swiftBic, result.SwiftBic);
    }

    [Fact]
    public void ToDomain_ShouldMapAccountTypeIdCorrectly()
    {
        const int accountTypeId = 3;
        var db = CreateValidDbAccount(accountTypeId: accountTypeId);

        var result = db.ToDomain();

        Assert.Equal(accountTypeId, result.AccountTypeId);
    }

    [Fact]
    public void ToDomain_ShouldMapBalanceCorrectly()
    {
        const decimal balance = 2500.50m;
        var db = CreateValidDbAccount(balance: balance);

        var result = db.ToDomain();

        Assert.Equal(balance, result.Balance);
    }

    [Fact]
    public void ToDomain_ShouldMapCurrencyCorrectly()
    {
        const string currency = "GBP";
        var db = CreateValidDbAccount(currency: currency);

        var result = db.ToDomain();

        Assert.Equal(currency, result.Currency);
    }

    [Fact]
    public void ToDomain_ShouldMapInstitutionIdCorrectly()
    {
        const int institutionId = 7;
        var db = CreateValidDbAccount(institutionId: institutionId);

        var result = db.ToDomain();

        Assert.Equal(institutionId, result.InstitutionId);
    }

    [Fact]
    public void ToDomain_ShouldMapIsActiveCorrectly()
    {
        const bool isActive = false;
        var db = CreateValidDbAccount(isActive: isActive);

        var result = db.ToDomain();

        Assert.Equal(isActive, result.IsActive);
    }

    [Fact]
    public void ToDomain_ShouldMapOpenedAtCorrectly()
    {
        var openedAt = new DateOnly(2023, 3, 20);
        var db = CreateValidDbAccount(openedAt: openedAt);

        var result = db.ToDomain();

        Assert.Equal(openedAt, result.OpenedAt);
    }

    [Fact]
    public void ToDomain_ShouldMapClosedAtCorrectly()
    {
        var closedAt = new DateOnly(2025, 6, 30);
        var db = CreateValidDbAccount(closedAt: closedAt);

        var result = db.ToDomain();

        Assert.Equal(closedAt, result.ClosedAt);
    }

    [Fact]
    public void ToDomain_ShouldMapCreatedAtCorrectly()
    {
        var createdAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var db = CreateValidDbAccount(createdAt: createdAt);

        var result = db.ToDomain();

        Assert.Equal(createdAt, result.CreatedAt);
    }

    [Fact]
    public void ToDomain_ShouldMapModifiedAtCorrectly()
    {
        var modifiedAt = new DateTime(2024, 12, 15, 18, 45, 0, DateTimeKind.Utc);
        var db = CreateValidDbAccount(modifiedAt: modifiedAt);

        var result = db.ToDomain();

        Assert.Equal(modifiedAt, result.ModifiedAt);
    }

    [Fact]
    public void ToDomain_WithNullOptionalFields_ShouldMapNullsCorrectly()
    {
        var db = CreateValidDbAccount(
            accountNumber: null,
            iban: null,
            swiftBic: null,
            openedAt: null,
            closedAt: null,
            modifiedAt: null);

        var result = db.ToDomain();

        Assert.Null(result.AccountNumber);
        Assert.Null(result.Iban);
        Assert.Null(result.SwiftBic);
        Assert.Null(result.OpenedAt);
        Assert.Null(result.ClosedAt);
        Assert.Null(result.ModifiedAt);
    }

    [Fact]
    public void ToDomain_ShouldReturnDomainAccountInstance()
    {
        var db = CreateValidDbAccount();

        var result = db.ToDomain();

        Assert.IsType<DomainAccount>(result);
    }

    #endregion

    #region Round-trip Tests

    [Fact]
    public void RoundTrip_ToDomainThenToDb_ShouldPreserveAllValues()
    {
        var originalDb = CreateValidDbAccount(
            id: 5,
            name: "Test Account",
            accountNumber: "ACC123",
            iban: "PT50000201231234567890154",
            swiftBic: "BPOTPTPL",
            accountTypeId: 2,
            balance: 5000m,
            currency: "EUR",
            institutionId: 3,
            isActive: true,
            openedAt: new DateOnly(2024, 1, 1),
            closedAt: null,
            createdAt: DateTime.UtcNow,
            modifiedAt: null);

        var domain = originalDb.ToDomain();
        var resultDb = domain.ToDb();

        Assert.Equal(originalDb.Id, resultDb.Id);
        Assert.Equal(originalDb.Name, resultDb.Name);
        Assert.Equal(originalDb.AccountNumber, resultDb.AccountNumber);
        Assert.Equal(originalDb.Iban, resultDb.Iban);
        Assert.Equal(originalDb.SwiftBic, resultDb.SwiftBic);
        Assert.Equal(originalDb.AccountTypeId, resultDb.AccountTypeId);
        Assert.Equal(originalDb.Balance, resultDb.Balance);
        Assert.Equal(originalDb.Currency, resultDb.Currency);
        Assert.Equal(originalDb.InstitutionId, resultDb.InstitutionId);
        Assert.Equal(originalDb.IsActive, resultDb.IsActive);
        Assert.Equal(originalDb.OpenedAt, resultDb.OpenedAt);
        Assert.Equal(originalDb.ClosedAt, resultDb.ClosedAt);
        Assert.Equal(originalDb.CreatedAt, resultDb.CreatedAt);
        Assert.Equal(originalDb.ModifiedAt, resultDb.ModifiedAt);
    }

    #endregion

    #region Helper Methods

    private static DomainAccount CreateValidDomainAccount(
        int id = 1,
        string name = "Test Account",
        string? accountNumber = "123456",
        string? iban = "PT50000201231234567890154",
        string? swiftBic = "BPOTPTPL",
        int accountTypeId = 1,
        decimal balance = 1000m,
        string currency = "EUR",
        int institutionId = 1,
        bool isActive = true,
        DateOnly? openedAt = null,
        DateOnly? closedAt = null,
        DateTime? createdAt = null,
        DateTime? modifiedAt = null)
    {
        return new DomainAccount(
            id,
            name,
            accountNumber,
            iban,
            swiftBic,
            accountTypeId,
            balance,
            currency,
            institutionId,
            isActive,
            openedAt,
            closedAt,
            createdAt ?? DateTime.UtcNow,
            modifiedAt);
    }

    private static DbAccount CreateValidDbAccount(
        int id = 1,
        string name = "Test Account",
        string? accountNumber = "123456",
        string? iban = "PT50000201231234567890154",
        string? swiftBic = "BPOTPTPL",
        int accountTypeId = 1,
        decimal balance = 1000m,
        string currency = "EUR",
        int institutionId = 1,
        bool isActive = true,
        DateOnly? openedAt = null,
        DateOnly? closedAt = null,
        DateTime? createdAt = null,
        DateTime? modifiedAt = null)
    {
        return new DbAccount
        {
            Id = id,
            Name = name,
            AccountNumber = accountNumber,
            Iban = iban,
            SwiftBic = swiftBic,
            AccountTypeId = accountTypeId,
            Balance = balance,
            Currency = currency,
            InstitutionId = institutionId,
            IsActive = isActive,
            OpenedAt = openedAt,
            ClosedAt = closedAt,
            CreatedAt = createdAt ?? DateTime.UtcNow,
            ModifiedAt = modifiedAt
        };
    }

    #endregion
}

