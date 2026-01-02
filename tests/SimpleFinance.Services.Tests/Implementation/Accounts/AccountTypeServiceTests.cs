using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Implementation.Accounts;

namespace SimpleFinance.Services.Tests.Implementation.Accounts;

public class AccountTypeServiceTests
{
    private readonly IAccountTypeRepository _repository;
    private readonly AccountTypeService _service;

    public AccountTypeServiceTests()
    {
        _repository = Substitute.For<IAccountTypeRepository>();
        _service = new AccountTypeService(_repository);
    }

    #region AddAccountTypeAsync Tests

    [Fact]
    public async Task AddAccountTypeAsync_ShouldCallRepository()
    {
        var accountType = new AccountType(0, "Savings", "Savings account");
        _repository.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _service.AddAccountTypeAsync(accountType, CancellationToken.None);

        await _repository.Received(1).AddAccountTypeAsync(accountType, CancellationToken.None);
    }

    [Fact]
    public async Task AddAccountTypeAsync_ShouldReturnRepositoryResult()
    {
        var accountType = new AccountType(0, "Checking");
        var savedAccountType = new AccountType(1, "Checking");
        _repository.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        var result = await _service.AddAccountTypeAsync(accountType, CancellationToken.None);

        Assert.Equal(savedAccountType, result);
    }

    [Fact]
    public async Task AddAccountTypeAsync_ShouldPassCancellationToken()
    {
        var accountType = new AccountType(0, "Investment");
        var cancellationToken = new CancellationToken();
        _repository.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _service.AddAccountTypeAsync(accountType, cancellationToken);

        await _repository.Received(1).AddAccountTypeAsync(accountType, cancellationToken);
    }

    #endregion

    #region GetAllAccountTypesAsync Tests

    [Fact]
    public async Task GetAllAccountTypesAsync_ShouldCallRepository()
    {
        _repository.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        await _service.GetAllAccountTypesAsync(CancellationToken.None);

        await _repository.Received(1).GetAllAccountTypesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_ShouldReturnRepositoryResult()
    {
        var accountTypes = new List<AccountType>
        {
            new(1, "Savings", "Savings account"),
            new(2, "Checking", "Checking account")
        };
        _repository.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(accountTypes);

        var result = await _service.GetAllAccountTypesAsync(CancellationToken.None);

        Assert.Equal(accountTypes, result);
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_ShouldPassCancellationToken()
    {
        var cancellationToken = new CancellationToken();
        _repository.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        await _service.GetAllAccountTypesAsync(cancellationToken);

        await _repository.Received(1).GetAllAccountTypesAsync(cancellationToken);
    }

    #endregion

    #region GetAccountTypeByIdAsync Tests

    [Fact]
    public async Task GetAccountTypeByIdAsync_ShouldCallRepository()
    {
        const int id = 1;
        _repository.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new AccountType(id, "Savings"));

        await _service.GetAccountTypeByIdAsync(id, CancellationToken.None);

        await _repository.Received(1).GetAccountTypeByIdAsync(id, CancellationToken.None);
    }

    [Fact]
    public async Task GetAccountTypeByIdAsync_ShouldReturnRepositoryResult()
    {
        const int id = 1;
        var accountType = new AccountType(id, "Checking", "Checking account");
        _repository.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        var result = await _service.GetAccountTypeByIdAsync(id, CancellationToken.None);

        Assert.Equal(accountType, result);
    }

    [Fact]
    public async Task GetAccountTypeByIdAsync_ShouldPassCancellationToken()
    {
        const int id = 1;
        var cancellationToken = new CancellationToken();
        _repository.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new AccountType(id, "Savings"));

        await _service.GetAccountTypeByIdAsync(id, cancellationToken);

        await _repository.Received(1).GetAccountTypeByIdAsync(id, cancellationToken);
    }

    [Fact]
    public async Task GetAccountTypeByIdAsync_ShouldPassCorrectId()
    {
        const int id = 42;
        _repository.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new AccountType(id, "Investment"));

        await _service.GetAccountTypeByIdAsync(id, CancellationToken.None);

        await _repository.Received(1).GetAccountTypeByIdAsync(id, Arg.Any<CancellationToken>());
    }

    #endregion

    #region UpdateAccountTypeAsync Tests

    [Fact]
    public async Task UpdateAccountTypeAsync_ShouldCallRepository()
    {
        var accountType = new AccountType(1, "Updated Name");
        _repository.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _service.UpdateAccountTypeAsync(accountType, CancellationToken.None);

        await _repository.Received(1).UpdateAccountTypeAsync(accountType, CancellationToken.None);
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_ShouldReturnRepositoryResult()
    {
        var accountType = new AccountType(1, "Original");
        var updatedAccountType = new AccountType(1, "Updated", "Updated description");
        _repository.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        var result = await _service.UpdateAccountTypeAsync(accountType, CancellationToken.None);

        Assert.Equal(updatedAccountType, result);
    }

    [Fact]
    public async Task UpdateAccountTypeAsync_ShouldPassCancellationToken()
    {
        var accountType = new AccountType(1, "Test");
        var cancellationToken = new CancellationToken();
        _repository.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _service.UpdateAccountTypeAsync(accountType, cancellationToken);

        await _repository.Received(1).UpdateAccountTypeAsync(accountType, cancellationToken);
    }

    #endregion

    #region DeleteAccountTypeAsync Tests

    [Fact]
    public async Task DeleteAccountTypeAsync_ShouldCallRepository()
    {
        const int id = 1;
        _repository.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountTypeAsync(id, CancellationToken.None);

        await _repository.Received(1).DeleteAccountTypeAsync(id, CancellationToken.None);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_ShouldReturnRepositoryResult_WhenTrue()
    {
        const int id = 1;
        _repository.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var result = await _service.DeleteAccountTypeAsync(id, CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_ShouldReturnRepositoryResult_WhenFalse()
    {
        const int id = 1;
        _repository.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _service.DeleteAccountTypeAsync(id, CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_ShouldPassCancellationToken()
    {
        const int id = 1;
        var cancellationToken = new CancellationToken();
        _repository.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountTypeAsync(id, cancellationToken);

        await _repository.Received(1).DeleteAccountTypeAsync(id, cancellationToken);
    }

    [Fact]
    public async Task DeleteAccountTypeAsync_ShouldPassCorrectId()
    {
        const int id = 42;
        _repository.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountTypeAsync(id, CancellationToken.None);

        await _repository.Received(1).DeleteAccountTypeAsync(id, Arg.Any<CancellationToken>());
    }

    #endregion
}

