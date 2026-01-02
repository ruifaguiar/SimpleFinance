using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Implementation.Accounts;

namespace SimpleFinance.Services.Tests.Implementation.Accounts;

public class AccountServiceTests
{
    private readonly IAccountRepository _repository;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _repository = Substitute.For<IAccountRepository>();
        _service = new AccountService(_repository);
    }

    #region AddAccountAsync Tests

    [Fact]
    public async Task AddAccountAsync_ShouldCallRepository()
    {
        var account = CreateValidAccount();
        _repository.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(account);

        await _service.AddAccountAsync(account, CancellationToken.None);

        await _repository.Received(1).AddAccountAsync(account, CancellationToken.None);
    }

    [Fact]
    public async Task AddAccountAsync_ShouldReturnRepositoryResult()
    {
        var account = CreateValidAccount();
        var savedAccount = CreateValidAccount(id: 1);
        _repository.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        var result = await _service.AddAccountAsync(account, CancellationToken.None);

        Assert.Equal(savedAccount, result);
    }

    [Fact]
    public async Task AddAccountAsync_ShouldPassCancellationToken()
    {
        var account = CreateValidAccount();
        var cancellationToken = new CancellationToken();
        _repository.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(account);

        await _service.AddAccountAsync(account, cancellationToken);

        await _repository.Received(1).AddAccountAsync(account, cancellationToken);
    }

    #endregion

    #region GetAllAccountsAsync Tests

    [Fact]
    public async Task GetAllAccountsAsync_ShouldCallRepository()
    {
        _repository.GetAllAccountsAsync(Arg.Any<CancellationToken>())
            .Returns(new List<Account>());

        await _service.GetAllAccountsAsync(CancellationToken.None);

        await _repository.Received(1).GetAllAccountsAsync(CancellationToken.None);
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldReturnRepositoryResult()
    {
        var accounts = new List<Account>
        {
            CreateValidAccount(id: 1, name: "Account 1"),
            CreateValidAccount(id: 2, name: "Account 2")
        };
        _repository.GetAllAccountsAsync(Arg.Any<CancellationToken>())
            .Returns(accounts);

        var result = await _service.GetAllAccountsAsync(CancellationToken.None);

        Assert.Equal(accounts, result);
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldPassCancellationToken()
    {
        var cancellationToken = new CancellationToken();
        _repository.GetAllAccountsAsync(Arg.Any<CancellationToken>())
            .Returns(new List<Account>());

        await _service.GetAllAccountsAsync(cancellationToken);

        await _repository.Received(1).GetAllAccountsAsync(cancellationToken);
    }

    #endregion

    #region GetAccountByIdAsync Tests

    [Fact]
    public async Task GetAccountByIdAsync_ShouldCallRepository()
    {
        const int id = 1;
        _repository.GetAccountByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(CreateValidAccount(id: id));

        await _service.GetAccountByIdAsync(id, CancellationToken.None);

        await _repository.Received(1).GetAccountByIdAsync(id, CancellationToken.None);
    }

    [Fact]
    public async Task GetAccountByIdAsync_ShouldReturnRepositoryResult()
    {
        const int id = 1;
        var account = CreateValidAccount(id: id);
        _repository.GetAccountByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(account);

        var result = await _service.GetAccountByIdAsync(id, CancellationToken.None);

        Assert.Equal(account, result);
    }

    [Fact]
    public async Task GetAccountByIdAsync_ShouldPassCancellationToken()
    {
        const int id = 1;
        var cancellationToken = new CancellationToken();
        _repository.GetAccountByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(CreateValidAccount(id: id));

        await _service.GetAccountByIdAsync(id, cancellationToken);

        await _repository.Received(1).GetAccountByIdAsync(id, cancellationToken);
    }

    [Fact]
    public async Task GetAccountByIdAsync_ShouldPassCorrectId()
    {
        const int id = 42;
        _repository.GetAccountByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(CreateValidAccount(id: id));

        await _service.GetAccountByIdAsync(id, CancellationToken.None);

        await _repository.Received(1).GetAccountByIdAsync(id, Arg.Any<CancellationToken>());
    }

    #endregion

    #region UpdateAccountAsync Tests

    [Fact]
    public async Task UpdateAccountAsync_ShouldCallRepository()
    {
        var account = CreateValidAccount(id: 1);
        _repository.UpdateAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(account);

        await _service.UpdateAccountAsync(account, CancellationToken.None);

        await _repository.Received(1).UpdateAccountAsync(account, CancellationToken.None);
    }

    [Fact]
    public async Task UpdateAccountAsync_ShouldReturnRepositoryResult()
    {
        var account = CreateValidAccount(id: 1, name: "Original");
        var updatedAccount = CreateValidAccount(id: 1, name: "Updated");
        _repository.UpdateAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccount);

        var result = await _service.UpdateAccountAsync(account, CancellationToken.None);

        Assert.Equal(updatedAccount, result);
    }

    [Fact]
    public async Task UpdateAccountAsync_ShouldPassCancellationToken()
    {
        var account = CreateValidAccount(id: 1);
        var cancellationToken = new CancellationToken();
        _repository.UpdateAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(account);

        await _service.UpdateAccountAsync(account, cancellationToken);

        await _repository.Received(1).UpdateAccountAsync(account, cancellationToken);
    }

    #endregion

    #region DeleteAccountAsync Tests

    [Fact]
    public async Task DeleteAccountAsync_ShouldCallRepository()
    {
        const int id = 1;
        _repository.DeleteAccountAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountAsync(id, CancellationToken.None);

        await _repository.Received(1).DeleteAccountAsync(id, CancellationToken.None);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldReturnRepositoryResult_WhenTrue()
    {
        const int id = 1;
        _repository.DeleteAccountAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var result = await _service.DeleteAccountAsync(id, CancellationToken.None);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldReturnRepositoryResult_WhenFalse()
    {
        const int id = 1;
        _repository.DeleteAccountAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _service.DeleteAccountAsync(id, CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldPassCancellationToken()
    {
        const int id = 1;
        var cancellationToken = new CancellationToken();
        _repository.DeleteAccountAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountAsync(id, cancellationToken);

        await _repository.Received(1).DeleteAccountAsync(id, cancellationToken);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldPassCorrectId()
    {
        const int id = 42;
        _repository.DeleteAccountAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _service.DeleteAccountAsync(id, CancellationToken.None);

        await _repository.Received(1).DeleteAccountAsync(id, Arg.Any<CancellationToken>());
    }

    #endregion

    #region Helper Methods

    private static Account CreateValidAccount(
        int id = 0,
        string name = "Test Account",
        string? accountNumber = "123456",
        string? iban = "PT50000201231234567890154",
        string? swiftBic = "BPOTPTPL",
        int accountTypeId = 1,
        decimal balance = 1000m,
        string currency = "EUR",
        int institutionId = 1,
        bool isActive = true)
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
            null,
            null,
            DateTime.UtcNow,
            null);
    }

    #endregion
}

