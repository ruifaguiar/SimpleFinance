using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Handlers;

public class DeleteAccountTypeHandlerTests
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly ILogger<DeleteAccountTypeHandler> _logger;
    private readonly DeleteAccountTypeHandler _handler;

    public DeleteAccountTypeHandlerTests()
    {
        _accountTypeService = Substitute.For<IAccountTypeService>();
        _logger = Substitute.For<ILogger<DeleteAccountTypeHandler>>();
        _handler = new DeleteAccountTypeHandler(_accountTypeService, _logger);
    }

    [Fact]
    public async Task HandleAsync_WithExistingId_ShouldCallService()
    {
        const int accountTypeId = 1;
        _accountTypeService.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        await _accountTypeService.Received(1).DeleteAccountTypeAsync(accountTypeId, CancellationToken.None);
    }

    [Fact]
    public async Task HandleAsync_WithExistingId_ShouldReturnNoContent()
    {
        const int accountTypeId = 1;
        _accountTypeService.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        Assert.IsType<NoContent>(result);
    }

    [Fact]
    public async Task HandleAsync_WhenNotFound_ShouldReturnNotFound()
    {
        const int accountTypeId = 999;
        _accountTypeService.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns<bool>(x => throw new NotFoundException("Not found"));

        var result = await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCorrectId()
    {
        const int accountTypeId = 42;
        _accountTypeService.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _handler.HandleAsync(accountTypeId, CancellationToken.None);

        await _accountTypeService.Received(1).DeleteAccountTypeAsync(accountTypeId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationToken()
    {
        const int accountTypeId = 1;
        var cancellationToken = new CancellationToken();
        _accountTypeService.DeleteAccountTypeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(true);

        await _handler.HandleAsync(accountTypeId, cancellationToken);

        await _accountTypeService.Received(1).DeleteAccountTypeAsync(Arg.Any<int>(), cancellationToken);
    }
}

