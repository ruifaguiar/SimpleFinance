using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Endpoints.Institutions.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Tests.Endpoints.Institutions.Handlers;

public class AddInstitutionHandlerTests
{
    private readonly IInstitutionService _institutionService;
    private readonly AddInstitutionHandler _handler;

    public AddInstitutionHandlerTests()
    {
        _institutionService = Substitute.For<IInstitutionService>();
        _handler = new AddInstitutionHandler(_institutionService);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldCallInstitutionService()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        await _handler.HandleAsync(model, CancellationToken.None);

        await _institutionService.Received(1).AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCreatedAtRouteResult()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        Assert.IsType<CreatedAtRoute<InstitutionModel>>(result);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnCorrectRouteName()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<InstitutionModel>>(result);
        Assert.Equal("GetInstitutionById", createdResult.RouteName);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnInstitutionIdInRouteValues()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<InstitutionModel>>(result);
        Assert.NotNull(createdResult.RouteValues);
        Assert.Equal(savedInstitution.Id, createdResult.RouteValues["institutionId"]);
    }

    [Fact]
    public async Task HandleAsync_WithValidModel_ShouldReturnSavedInstitutionInBody()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        var result = await _handler.HandleAsync(model, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtRoute<InstitutionModel>>(result);
        Assert.NotNull(createdResult.Value);
        Assert.Equal(savedInstitution.Id, createdResult.Value.Id);
        Assert.Equal(savedInstitution.Name, createdResult.Value.Name);
    }

    [Fact]
    public async Task HandleAsync_ShouldPassCancellationTokenToService()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        var savedInstitution = new Institution(1, "Test Bank");
        var cancellationToken = CancellationToken.None;
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        await _handler.HandleAsync(model, cancellationToken);

        await _institutionService.Received(1).AddInstitutionAsync(Arg.Any<Institution>(), cancellationToken);
    }

    [Fact]
    public async Task HandleAsync_ShouldMapModelToDomainCorrectly()
    {
        var model = new InstitutionModel { Name = "Test Bank Name" };
        var savedInstitution = new Institution(1, "Test Bank Name");
        Institution? capturedInstitution = null;
        _institutionService.AddInstitutionAsync(Arg.Do<Institution>(i => capturedInstitution = i), Arg.Any<CancellationToken>())
            .Returns(savedInstitution);

        await _handler.HandleAsync(model, CancellationToken.None);

        Assert.NotNull(capturedInstitution);
        Assert.Equal(model.Name, capturedInstitution.Name);
    }

    [Fact]
    public async Task HandleAsync_WhenServiceThrowsException_ShouldPropagateException()
    {
        var model = new InstitutionModel { Name = "Test Bank" };
        _institutionService.AddInstitutionAsync(Arg.Any<Institution>(), Arg.Any<CancellationToken>())
            .Returns<Institution>(_ => throw new InvalidOperationException("Service error"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.HandleAsync(model, CancellationToken.None));
    }
}

