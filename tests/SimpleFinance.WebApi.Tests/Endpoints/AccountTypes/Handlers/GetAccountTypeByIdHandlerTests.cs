using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Handlers;

public class GetAccountTypeByIdHandlerTests
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly GetAccountTypeByIdHandler _handler;

    public GetAccountTypeByIdHandlerTests()
    {
        _accountTypeService = Substitute.For<IAccountTypeService>();
        _handler = new GetAccountTypeByIdHandler(_accountTypeService);
    }

    [Fact]
    public async Task HandleAsync_WithExistingId_ShouldCallService()
    {
        const int accountTypeId = 1;
        var accountType = new AccountType(accountTypeId, "Savings");
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        await _accountTypeService.Received(1).GetAccountTypeByIdAsync(accountTypeId, CancellationToken.None);
    }

    [Fact]
    public async Task HandleAsync_WithExistingId_ShouldReturnOkResult()
    {
        const int accountTypeId = 1;
        var accountType = new AccountType(accountTypeId, "Savings");
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        Assert.IsType<Ok<AccountTypeModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithExistingId_ShouldReturnCorrectAccountType()
    {
        const int accountTypeId = 1;
        var accountType = new AccountType(accountTypeId, "Savings", "Savings account");
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        var okResult = Assert.IsType<Ok<AccountTypeModel>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(accountTypeId, okResult.Value.Id);
        Assert.Equal("Savings", okResult.Value.Name);
        Assert.Equal("Savings account", okResult.Value.Description);
    }

    [Fact]
    public async Task HandleAsync_WhenNotFound_ShouldReturnNotFound()
    {
        const int accountTypeId = 999;
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns<AccountType>(x => throw new NotFoundException("Not found"));

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCorrectId()
    {
        const int accountTypeId = 42;
        var accountType = new AccountType(accountTypeId, "Test");
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        await _accountTypeService.Received(1).GetAccountTypeByIdAsync(accountTypeId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationToken()
    {
        const int accountTypeId = 1;
        var cancellationToken = new CancellationToken();
        var accountType = new AccountType(accountTypeId, "Test");
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        await _handler.HandleAsync(accountTypeId, cancellationToken);

        await _accountTypeService.Received(1).GetAccountTypeByIdAsync(Arg.Any<int>(), cancellationToken);
    }

    [Fact]
    public async Task HandleAsync_WithNullDescription_ShouldMapCorrectly()
    {
        const int accountTypeId = 1;
        var accountType = new AccountType(accountTypeId, "Basic", null);
        _accountTypeService.GetAccountTypeByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(accountType);

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        var okResult = Assert.IsType<Ok<AccountTypeModel>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Null(okResult.Value.Description);
    }
}

