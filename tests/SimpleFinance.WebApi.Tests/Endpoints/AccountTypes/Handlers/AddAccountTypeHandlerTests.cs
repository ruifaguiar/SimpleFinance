using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Handlers;

public class AddAccountTypeHandlerTests
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly AddAccountTypeHandler _handler;

    public AddAccountTypeHandlerTests()
    {
        _accountTypeService = Substitute.For<IAccountTypeService>();
        _handler = new AddAccountTypeHandler(_accountTypeService);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldCallAccountTypeService()
    {
        var model = new AccountTypeModel { Name = "Savings", Description = "Savings account" };
        var savedAccountType = new AccountType(1, "Savings", "Savings account");
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        await _handler.HandleAsync(model, CancellationToken.None);

        await _accountTypeService.Received(1).AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCreatedAtRouteResult()
    {
        var model = new AccountTypeModel { Name = "Checking" };
        var savedAccountType = new AccountType(1, "Checking");
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        Assert.IsType<CreatedAtRoute<AccountTypeModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCorrectRouteName()
    {
        var model = new AccountTypeModel { Name = "Investment" };
        var savedAccountType = new AccountType(1, "Investment");
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountTypeModel>>(result);
        Assert.Equal("GetAccountTypeById", createdResult.RouteName);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnAccountTypeIdInRouteValues()
    {
        var model = new AccountTypeModel { Name = "Credit" };
        var savedAccountType = new AccountType(42, "Credit");
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountTypeModel>>(result);
        Assert.NotNull(createdResult.RouteValues);
        Assert.Equal(savedAccountType.Id, createdResult.RouteValues["accountTypeId"]);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnSavedAccountTypeInBody()
    {
        var model = new AccountTypeModel { Name = "Savings", Description = "Savings account" };
        var savedAccountType = new AccountType(1, "Savings", "Savings account");
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<AccountTypeModel>>(result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(savedAccountType.Id, createdResult.Value.Id);
        Assert.Equal(savedAccountType.Name, createdResult.Value.Name);
        Assert.Equal(savedAccountType.Description, createdResult.Value.Description);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationTokenToService()
    {
        var model = new AccountTypeModel { Name = "Test" };
        var savedAccountType = new AccountType(1, "Test");
        var cancellationToken = new CancellationToken();
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        await _handler.HandleAsync(model, cancellationToken);

        await _accountTypeService.Received(1).AddAccountTypeAsync(Arg.Any<AccountType>(), cancellationToken);
    }

    [Fact]
    public async Task HandleAsync_ShouldMapModelToDomainCorrectly()
    {
        var model = new AccountTypeModel { Name = "Savings", Description = "Savings description" };
        var savedAccountType = new AccountType(1, "Savings", "Savings description");
        AccountType? capturedAccountType = null;
        _accountTypeService.AddAccountTypeAsync(Arg.Do<AccountType>(at => capturedAccountType = at), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        await _handler.HandleAsync(model, CancellationToken.None);

        Assert.NotNull(capturedAccountType);
        Assert.Equal(model.Name, capturedAccountType.Name);
        Assert.Equal(model.Description, capturedAccountType.Description);
    }

    [Fact]
    public async Task HandleAsync_WithNullDescription_ShouldMapCorrectly()
    {
        var model = new AccountTypeModel { Name = "Basic", Description = null };
        var savedAccountType = new AccountType(1, "Basic", null);
        AccountType? capturedAccountType = null;
        _accountTypeService.AddAccountTypeAsync(Arg.Do<AccountType>(at => capturedAccountType = at), Arg.Any<CancellationToken>())
            .Returns(savedAccountType);

        await _handler.HandleAsync(model, CancellationToken.None);

        Assert.NotNull(capturedAccountType);
        Assert.Equal(model.Name, capturedAccountType.Name);
        Assert.Null(capturedAccountType.Description);
    }

    [Fact]
    public async Task HandleAsync_WhenServiceThrowsException_ShouldPropagateException()
    {
        var model = new AccountTypeModel { Name = "Test" };
        _accountTypeService.AddAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns<AccountType>(x => throw new InvalidOperationException("Service error"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.HandleAsync(model, CancellationToken.None));
    }
}

