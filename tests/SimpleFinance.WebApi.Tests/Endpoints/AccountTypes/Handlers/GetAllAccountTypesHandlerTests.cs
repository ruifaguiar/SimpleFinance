using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Handlers;

public class GetAllAccountTypesHandlerTests
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly GetAllAccountTypesHandler _handler;

    public GetAllAccountTypesHandlerTests()
    {
        _accountTypeService = Substitute.For<IAccountTypeService>();
        _handler = new GetAllAccountTypesHandler(_accountTypeService);
    }

    [Fact]
    public async Task HandleAsync_ShouldCallService()
    {
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        await _handler.HandleAsync(CancellationToken.None);

        await _accountTypeService.Received(1).GetAllAccountTypesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnOkResult()
    {
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        var result = await _handler.HandleAsync(CancellationToken.None);

        Assert.IsType<Ok<IEnumerable<AccountTypeModel>>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithNoAccountTypes_ShouldReturnEmptyList()
    {
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        var result = await _handler.HandleAsync(CancellationToken.None);

        var okResult = Assert.IsType<Ok<IEnumerable<AccountTypeModel>>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Empty(okResult.Value);
    }

    [Fact]
    public async Task HandleAsync_WithAccountTypes_ShouldReturnAllAccountTypes()
    {
        var accountTypes = new List<AccountType>
        {
            new(1, "Savings", "Savings account"),
            new(2, "Checking", "Checking account"),
            new(3, "Investment")
        };
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(accountTypes);

        var result = await _handler.HandleAsync(CancellationToken.None);

        var okResult = Assert.IsType<Ok<IEnumerable<AccountTypeModel>>>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(3, okResult.Value.Count());
    }

    [Fact]
    public async Task HandleAsync_ShouldMapAccountTypesCorrectly()
    {
        var accountTypes = new List<AccountType>
        {
            new(1, "Savings", "Savings account"),
            new(2, "Checking", null)
        };
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(accountTypes);

        var result = await _handler.HandleAsync(CancellationToken.None);

        var okResult = Assert.IsType<Ok<IEnumerable<AccountTypeModel>>>(result);
        Assert.NotNull(okResult.Value);
        var models = okResult.Value.ToList();
        
        Assert.Equal(1, models[0].Id);
        Assert.Equal("Savings", models[0].Name);
        Assert.Equal("Savings account", models[0].Description);
        
        Assert.Equal(2, models[1].Id);
        Assert.Equal("Checking", models[1].Name);
        Assert.Null(models[1].Description);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationToken()
    {
        var cancellationToken = new CancellationToken();
        _accountTypeService.GetAllAccountTypesAsync(Arg.Any<CancellationToken>())
            .Returns(new List<AccountType>());

        await _handler.HandleAsync(cancellationToken);

        await _accountTypeService.Received(1).GetAllAccountTypesAsync(cancellationToken);
    }
}

