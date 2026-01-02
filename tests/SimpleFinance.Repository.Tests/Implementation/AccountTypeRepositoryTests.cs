using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Implementation;

namespace SimpleFinance.Repository.Tests.Implementation;

public class AccountTypeRepositoryTests : IDisposable
{
    private readonly SimpleFinanceDbContext _dbContext;
    private readonly AccountTypeRepository _repository;

    public AccountTypeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SimpleFinanceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new SimpleFinanceDbContext(options);
        _repository = new AccountTypeRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    #region AddAccountTypeAsync Tests

    [Fact]
    public async Task AddAccountTypeAsync_WithValidAccountType_ShouldAddToDatabase()
    {
        var accountType = new AccountType(0, "Savings", "Savings account type");

        await _repository.AddAccountTypeAsync(accountType, CancellationToken.None);

        var dbAccountType = await _dbContext.AccountTypes.FirstOrDefaultAsync();
        Assert.NotNull(dbAccountType);
        Assert.Equal("Savings", dbAccountType.Name);
        Assert.Equal("Savings account type", dbAccountType.Description);
    }

    [Fact]
    public async Task AddAccountTypeAsync_WithValidAccountType_ShouldReturnAccountType()
    {
        var accountType = new AccountType(0, "Checking", "Checking account type");

        var result = await _repository.AddAccountTypeAsync(accountType, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Checking", result.Name);
        Assert.Equal("Checking account type", result.Description);
    }

    [Fact]
    public async Task AddAccountTypeAsync_WithNullDescription_ShouldAddSuccessfully()
    {
        var accountType = new AccountType(0, "Investment");

        var result = await _repository.AddAccountTypeAsync(accountType, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Investment", result.Name);
        Assert.Null(result.Description);
    }

    #endregion

    #region GetAllAccountTypesAsync Tests

    [Fact]
    public async Task GetAllAccountTypesAsync_WithNoAccountTypes_ShouldReturnEmptyList()
    {
        var result = await _repository.GetAllAccountTypesAsync(CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_WithAccountTypes_ShouldReturnAllAccountTypes()
    {
        _dbContext.AccountTypes.AddRange(
            new Database.Entities.AccountType { Id = 1, Name = "Savings" },
            new Database.Entities.AccountType { Id = 2, Name = "Checking" },
            new Database.Entities.AccountType { Id = 3, Name = "Investment" }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAllAccountTypesAsync(CancellationToken.None);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_ShouldReturnDomainObjects()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType { Id = 1, Name = "Savings" });
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAllAccountTypesAsync(CancellationToken.None);

        Assert.All(result, at => Assert.IsType<AccountType>(at));
    }

    #endregion

    #region GetAccountTypeByIdAsync Tests

    [Fact]
    public async Task GetAccountTypeByIdAsync_WithExistingId_ShouldReturnAccountType()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType 
        { 
            Id = 1, 
            Name = "Savings", 
            Description = "Savings account" 
        });
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAccountTypeByIdAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Savings", result.Name);
        Assert.Equal("Savings account", result.Description);
    }

    [Fact]
    public async Task GetAccountTypeByIdAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.GetAccountTypeByIdAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task GetAccountTypeByIdAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.GetAccountTypeByIdAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion

    #region UpdateAccountTypeAsync Tests

    [Fact]
    public async Task UpdateAccountTypeAsync_WithExistingAccountType_ShouldUpdateName()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType { Id = 1, Name = "Old Name" });
        await _dbContext.SaveChangesAsync();
        var updatedAccountType = new AccountType(1, "New Name");

        await _repository.UpdateAccountTypeAsync(updatedAccountType, CancellationToken.None);

        var dbAccountType = await _dbContext.AccountTypes.FindAsync(1);
        Assert.Equal("New Name", dbAccountType!.Name);
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_WithExistingAccountType_ShouldUpdateDescription()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType 
        { 
            Id = 1, 
            Name = "Savings", 
            Description = "Old description" 
        });
        await _dbContext.SaveChangesAsync();
        var updatedAccountType = new AccountType(1, "Savings", "New description");

        await _repository.UpdateAccountTypeAsync(updatedAccountType, CancellationToken.None);

        var dbAccountType = await _dbContext.AccountTypes.FindAsync(1);
        Assert.Equal("New description", dbAccountType!.Description);
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_WithExistingAccountType_ShouldReturnUpdatedAccountType()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType { Id = 1, Name = "Old Name" });
        await _dbContext.SaveChangesAsync();
        var updatedAccountType = new AccountType(1, "New Name", "New description");

        var result = await _repository.UpdateAccountTypeAsync(updatedAccountType, CancellationToken.None);

        Assert.Equal("New Name", result.Name);
        Assert.Equal("New description", result.Description);
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        var accountType = new AccountType(999, "Test");

        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.UpdateAccountTypeAsync(accountType, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var accountType = new AccountType(999, "Test");

        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.UpdateAccountTypeAsync(accountType, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion

    #region DeleteAccountTypeAsync Tests

    [Fact]
    public async Task DeleteAccountTypeAsync_WithExistingId_ShouldRemoveFromDatabase()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType { Id = 1, Name = "Savings" });
        await _dbContext.SaveChangesAsync();

        await _repository.DeleteAccountTypeAsync(1, CancellationToken.None);

        var dbAccountType = await _dbContext.AccountTypes.FindAsync(1);
        Assert.Null(dbAccountType);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_WithExistingId_ShouldReturnTrue()
    {
        _dbContext.AccountTypes.Add(new Database.Entities.AccountType { Id = 1, Name = "Savings" });
        await _dbContext.SaveChangesAsync();

        var result = await _repository.DeleteAccountTypeAsync(1, CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.DeleteAccountTypeAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _repository.DeleteAccountTypeAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion
}

