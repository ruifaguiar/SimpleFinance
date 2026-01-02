using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.Accounts.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.Accounts.Handlers;

public class AddAccountHandlerTests
{
    private readonly IAccountService _accountService;
    private readonly AddAccountHandler _handler;

    public AddAccountHandlerTests()
    {
        _accountService = Substitute.For<IAccountService>();
        _handler = new AddAccountHandler(_accountService);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldCallAccountService()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        await _handler.HandleAsync(model, CancellationToken.None);

        await _accountService.Received(1).AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCreatedAtRouteResult()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        Assert.IsType<CreatedAtRoute<AccountModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCorrectRouteName()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountModel>>(result);
        Assert.Equal("GetAccountById", createdResult.RouteName);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnAccountIdInRouteValues()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountModel>>(result);
        Assert.NotNull(createdResult.RouteValues);
        Assert.Equal(savedAccount.Id, createdResult.RouteValues["accountId"]);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnSavedAccountInBody()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountModel>>(result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(savedAccount.Id, createdResult.Value.Id);
        Assert.Equal(savedAccount.Name, createdResult.Value.Name);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationTokenToService()
    {
        var model = CreateValidAccountModel();
        var savedAccount = CreateSavedAccount();
        var cancellationToken = CancellationToken.None;
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        await _handler.HandleAsync(model, cancellationToken);

        await _accountService.Received(1).AddAccountAsync(Arg.Any<Account>(), cancellationToken);
    }

    [Fact]
    public async Task HandleAsync_ShouldMapModelToDomainCorrectly()
    {
        var model = CreateValidAccountModel();
        model.Name = "Test Account Name";
        model.AccountTypeId = 5;
        model.InstitutionId = 10;
        var savedAccount = CreateSavedAccount();
        Account? capturedAccount = null;
        _accountService.AddAccountAsync(Arg.Do<Account>(a => capturedAccount = a), Arg.Any<CancellationToken>())
            .Returns(savedAccount);

        await _handler.HandleAsync(model, CancellationToken.None);

        Assert.NotNull(capturedAccount);
        Assert.Equal(model.Name, capturedAccount.Name);
        Assert.Equal(model.AccountTypeId, capturedAccount.AccountTypeId);
        Assert.Equal(model.InstitutionId, capturedAccount.InstitutionId);
    }

    [Fact]
    public async Task HandleAsync_WhenServiceThrowsException_ShouldPropagateException()
    {
        var model = CreateValidAccountModel();
        _accountService.AddAccountAsync(Arg.Any<Account>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("Service error"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.HandleAsync(model, CancellationToken.None));
    }

    private static AccountModel CreateValidAccountModel()
    {
        return new AccountModel
        {
            Name = "Test Account",
            AccountNumber = "123456789",
            Iban = "PT50000201231234567890154",
            SwiftBic = "BPOTPTPL",
            AccountTypeId = 1,
            Balance = 1000m,
            Currency = "EUR",
            InstitutionId = 1,
            IsActive = true,
            OpenedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };
    }

    private static Account CreateSavedAccount()
    {
        return new Account(
            id: 1,
            name: "Test Account",
            accountNumber: "123456789",
            iban: "PT50000201231234567890154",
            swiftBic: "BPOTPTPL",
            accountTypeId: 1,
            balance: 1000m,
            currency: "EUR",
            institutionId: 1,
            isActive: true,
            openedAt: DateOnly.FromDateTime(DateTime.UtcNow),
            closedAt: null,
            createdAt: DateTime.UtcNow,
            modifiedAt: null);
    }
}

