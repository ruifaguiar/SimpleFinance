using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Handlers;

public class UpdateAccountTypeHandlerTests
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly UpdateAccountTypeHandler _handler;

    public UpdateAccountTypeHandlerTests()
    {
        _accountTypeService = Substitute.For<IAccountTypeService>();
        var logger = Substitute.For<ILogger<UpdateAccountTypeHandler>>();
        _handler = new UpdateAccountTypeHandler(_accountTypeService, logger);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldCallService()
    {
        const int accountTypeId = 1;
        var model = new AccountTypeModel { Name = "Updated Name" };
        var updatedAccountType = new AccountType(accountTypeId, "Updated Name");
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        await _accountTypeService.Received(1).UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnOkResult()
    {
        const int accountTypeId = 1;
        var model = new AccountTypeModel { Name = "Updated Name" };
        var updatedAccountType = new AccountType(accountTypeId, "Updated Name");
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        var result = await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        Assert.IsType<Ok<AccountTypeModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnUpdatedAccountType()
    {
        const int accountTypeId = 1;
        var model = new AccountTypeModel { Name = "Updated Name", Description = "Updated Description" };
        var updatedAccountType = new AccountType(accountTypeId, "Updated Name", "Updated Description");
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        var result = await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        var okResult = Assert.IsType<Ok<AccountTypeModel>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(accountTypeId, okResult.Value.Id);
        Assert.Equal("Updated Name", okResult.Value.Name);
        Assert.Equal("Updated Description", okResult.Value.Description);
    }

    [Fact]
    public async Task HandleAsync_WithMismatchedIds_ShouldReturnBadRequest()
    {
        const int urlId = 1;
        var model = new AccountTypeModel { Id = 2, Name = "Test" };

        var result = await _handler.HandleAsync(urlId, model, CancellationToken.None);

        Assert.IsType<BadRequest<string>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithMismatchedIds_ShouldNotCallService()
    {
        const int urlId = 1;
        var model = new AccountTypeModel { Id = 2, Name = "Test" };

        await _handler.HandleAsync(urlId, model, CancellationToken.None);

        await _accountTypeService.DidNotReceive().UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WithNullModelId_ShouldSetIdFromUrl()
    {
        const int accountTypeId = 5;
        var model = new AccountTypeModel { Id = null, Name = "Test" };
        var updatedAccountType = new AccountType(accountTypeId, "Test");
        AccountType? capturedAccountType = null;
        _accountTypeService.UpdateAccountTypeAsync(Arg.Do<AccountType>(at => capturedAccountType = at), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        Assert.NotNull(capturedAccountType);
        Assert.Equal(accountTypeId, capturedAccountType.Id);
    }

    [Fact]
    public async Task HandleAsync_WithMatchingIds_ShouldProceed()
    {
        const int accountTypeId = 1;
        var model = new AccountTypeModel { Id = 1, Name = "Test" };
        var updatedAccountType = new AccountType(accountTypeId, "Test");
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        var result = await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        Assert.IsType<Ok<AccountTypeModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WhenNotFound_ShouldReturnNotFound()
    {
        const int accountTypeId = 999;
        var model = new AccountTypeModel { Name = "Test" };
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns<AccountType>(x => throw new NotFoundException("Not found"));

        var result = await _handler.HandleAsync(accountTypeId, model, CancellationToken.None);

        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationToken()
    {
        const int accountTypeId = 1;
        var model = new AccountTypeModel { Name = "Test" };
        var cancellationToken = new CancellationToken();
        var updatedAccountType = new AccountType(accountTypeId, "Test");
        _accountTypeService.UpdateAccountTypeAsync(Arg.Any<AccountType>(), Arg.Any<CancellationToken>())
            .Returns(updatedAccountType);

        await _handler.HandleAsync(accountTypeId, model, cancellationToken);

        await _accountTypeService.Received(1).UpdateAccountTypeAsync(Arg.Any<AccountType>(), cancellationToken);
    }
}

