using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Implementation;

namespace SimpleFinance.Repository.Tests.Implementation;

public class AccountRepositoryTests : IDisposable
{
    private readonly SimpleFinanceDbContext _dbContext;
    private readonly AccountRepository _repository;

    public AccountRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SimpleFinanceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new SimpleFinanceDbContext(options);
        _repository = new AccountRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    #region AddAccountAsync Tests

    [Fact]
    public async Task AddAccountAsync_WithValidAccount_ShouldAddToDatabase()
    {
        var account = CreateValidDomainAccount();

        await _repository.AddAccountAsync(account, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FirstOrDefaultAsync();
        Assert.NotNull(dbAccount);
        Assert.Equal("Test Account", dbAccount.Name);
    }

    [Fact]
    public async Task AddAccountAsync_WithValidAccount_ShouldReturnAccountWithId()
    {
        var account = CreateValidDomainAccount();

        var result = await _repository.AddAccountAsync(account, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Test Account", result.Name);
    }

    [Fact]
    public async Task AddAccountAsync_ShouldSetCreatedAt()
    {
        var account = CreateValidDomainAccount();
        var beforeAdd = DateTime.UtcNow;

        await _repository.AddAccountAsync(account, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FirstOrDefaultAsync();
        Assert.NotNull(dbAccount);
        Assert.True(dbAccount.CreatedAt >= beforeAdd);
    }

    [Fact]
    public async Task AddAccountAsync_ShouldMapAllFieldsCorrectly()
    {
        var account = CreateValidDomainAccount(
            name: "Savings Account",
            accountNumber: "123456789",
            iban: "PT50000201231234567890154",
            swiftBic: "BPOTPTPL",
            accountTypeId: 2,
            balance: 5000m,
            currency: "EUR",
            institutionId: 3,
            isActive: true);

        var result = await _repository.AddAccountAsync(account, CancellationToken.None);

        Assert.Equal("Savings Account", result.Name);
        Assert.Equal("123456789", result.AccountNumber);
        Assert.Equal("PT50000201231234567890154", result.Iban);
        Assert.Equal("BPOTPTPL", result.SwiftBic);
        Assert.Equal(2, result.AccountTypeId);
        Assert.Equal(5000m, result.Balance);
        Assert.Equal("EUR", result.Currency);
        Assert.Equal(3, result.InstitutionId);
        Assert.True(result.IsActive);
    }

    #endregion

    #region GetAllAccountsAsync Tests

    [Fact]
    public async Task GetAllAccountsAsync_WithNoAccounts_ShouldReturnEmptyList()
    {
        var result = await _repository.GetAllAccountsAsync(CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAccountsAsync_WithAccounts_ShouldReturnAllAccounts()
    {
        _dbContext.Accounts.AddRange(
            CreateValidDbAccount(id: 1, name: "Account 1"),
            CreateValidDbAccount(id: 2, name: "Account 2"),
            CreateValidDbAccount(id: 3, name: "Account 3")
        );
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAllAccountsAsync(CancellationToken.None);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldReturnDomainObjects()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Test Account"));
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAllAccountsAsync(CancellationToken.None);

        Assert.All(result, a => Assert.IsType<Account>(a));
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldMapAllFieldsCorrectly()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(
            id: 1,
            name: "Full Account",
            accountNumber: "987654321",
            iban: "DE89370400440532013000",
            swiftBic: "COBADEFF",
            accountTypeId: 5,
            balance: 10000m,
            currency: "USD",
            institutionId: 7,
            isActive: false));
        await _dbContext.SaveChangesAsync();

        var result = (await _repository.GetAllAccountsAsync(CancellationToken.None)).First();

        Assert.Equal("Full Account", result.Name);
        Assert.Equal("987654321", result.AccountNumber);
        Assert.Equal("DE89370400440532013000", result.Iban);
        Assert.Equal("COBADEFF", result.SwiftBic);
        Assert.Equal(5, result.AccountTypeId);
        Assert.Equal(10000m, result.Balance);
        Assert.Equal("USD", result.Currency);
        Assert.Equal(7, result.InstitutionId);
        Assert.False(result.IsActive);
    }

    #endregion

    #region GetAccountByIdAsync Tests

    [Fact]
    public async Task GetAccountByIdAsync_WithExistingId_ShouldReturnAccount()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Test Account"));
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAccountByIdAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Account", result.Name);
    }

    [Fact]
    public async Task GetAccountByIdAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.GetAccountByIdAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task GetAccountByIdAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.GetAccountByIdAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    [Fact]
    public async Task GetAccountByIdAsync_ShouldMapAllFieldsCorrectly()
    {
        var openedAt = new DateOnly(2024, 1, 1);
        var closedAt = new DateOnly(2025, 12, 31);
        _dbContext.Accounts.Add(CreateValidDbAccount(
            id: 1,
            name: "Complete Account",
            accountNumber: "ACC123",
            iban: "PT50000201231234567890154",
            swiftBic: "BPOTPTPL",
            accountTypeId: 2,
            balance: 7500m,
            currency: "GBP",
            institutionId: 4,
            isActive: true,
            openedAt: openedAt,
            closedAt: closedAt));
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAccountByIdAsync(1, CancellationToken.None);

        Assert.Equal("Complete Account", result.Name);
        Assert.Equal("ACC123", result.AccountNumber);
        Assert.Equal("PT50000201231234567890154", result.Iban);
        Assert.Equal("BPOTPTPL", result.SwiftBic);
        Assert.Equal(2, result.AccountTypeId);
        Assert.Equal(7500m, result.Balance);
        Assert.Equal("GBP", result.Currency);
        Assert.Equal(4, result.InstitutionId);
        Assert.True(result.IsActive);
        Assert.Equal(openedAt, result.OpenedAt);
        Assert.Equal(closedAt, result.ClosedAt);
    }

    #endregion

    #region UpdateAccountAsync Tests

    [Fact]
    public async Task UpdateAccountAsync_WithExistingAccount_ShouldUpdateName()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Old Name"));
        await _dbContext.SaveChangesAsync();
        var updatedAccount = CreateValidDomainAccount(id: 1, name: "New Name");

        await _repository.UpdateAccountAsync(updatedAccount, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FindAsync(1);
        Assert.Equal("New Name", dbAccount!.Name);
    }

    [Fact]
    public async Task UpdateAccountAsync_WithExistingAccount_ShouldUpdateAllFields()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Original"));
        await _dbContext.SaveChangesAsync();
        
        var updatedAccount = CreateValidDomainAccount(
            id: 1,
            name: "Updated Account",
            accountNumber: "NEW123",
            iban: "DE89370400440532013000",
            swiftBic: "COBADEFF",
            accountTypeId: 3,
            balance: 9999m,
            currency: "CHF",
            institutionId: 5,
            isActive: false,
            openedAt: new DateOnly(2024, 6, 15),
            closedAt: new DateOnly(2025, 6, 15));

        await _repository.UpdateAccountAsync(updatedAccount, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FindAsync(1);
        Assert.Equal("Updated Account", dbAccount!.Name);
        Assert.Equal("NEW123", dbAccount.AccountNumber);
        Assert.Equal("DE89370400440532013000", dbAccount.Iban);
        Assert.Equal("COBADEFF", dbAccount.SwiftBic);
        Assert.Equal(3, dbAccount.AccountTypeId);
        Assert.Equal(9999m, dbAccount.Balance);
        Assert.Equal("CHF", dbAccount.Currency);
        Assert.Equal(5, dbAccount.InstitutionId);
        Assert.False(dbAccount.IsActive);
        Assert.Equal(new DateOnly(2024, 6, 15), dbAccount.OpenedAt);
        Assert.Equal(new DateOnly(2025, 6, 15), dbAccount.ClosedAt);
    }

    [Fact]
    public async Task UpdateAccountAsync_WithExistingAccount_ShouldReturnUpdatedAccount()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Old Name"));
        await _dbContext.SaveChangesAsync();
        var updatedAccount = CreateValidDomainAccount(id: 1, name: "New Name");

        var result = await _repository.UpdateAccountAsync(updatedAccount, CancellationToken.None);

        Assert.Equal("New Name", result.Name);
    }

    [Fact]
    public async Task UpdateAccountAsync_ShouldSetModifiedAt()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Test"));
        await _dbContext.SaveChangesAsync();
        var updatedAccount = CreateValidDomainAccount(id: 1, name: "Updated");
        var beforeUpdate = DateTime.UtcNow;

        await _repository.UpdateAccountAsync(updatedAccount, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FindAsync(1);
        Assert.NotNull(dbAccount!.ModifiedAt);
        Assert.True(dbAccount.ModifiedAt >= beforeUpdate);
    }

    [Fact]
    public async Task UpdateAccountAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        var account = CreateValidDomainAccount(id: 999, name: "Test");

        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.UpdateAccountAsync(account, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAccountAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var account = CreateValidDomainAccount(id: 999, name: "Test");

        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.UpdateAccountAsync(account, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion

    #region DeleteAccountAsync Tests

    [Fact]
    public async Task DeleteAccountAsync_WithExistingId_ShouldRemoveFromDatabase()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Test Account"));
        await _dbContext.SaveChangesAsync();

        await _repository.DeleteAccountAsync(1, CancellationToken.None);

        var dbAccount = await _dbContext.Accounts.FindAsync(1);
        Assert.Null(dbAccount);
    }

    [Fact]
    public async Task DeleteAccountAsync_WithExistingId_ShouldReturnTrue()
    {
        _dbContext.Accounts.Add(CreateValidDbAccount(id: 1, name: "Test Account"));
        await _dbContext.SaveChangesAsync();

        var result = await _repository.DeleteAccountAsync(1, CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAccountAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.DeleteAccountAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteAccountAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.DeleteAccountAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion

    #region Helper Methods

    private static Account CreateValidDomainAccount(
        int id = 0,
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
        DateOnly? closedAt = null)
    {
        return new Account(
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
            DateTime.UtcNow,
            null);
    }

    private static Database.Entities.Account CreateValidDbAccount(
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
        DateOnly? closedAt = null)
    {
        return new Database.Entities.Account
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
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = null
        };
    }

    #endregion
}

