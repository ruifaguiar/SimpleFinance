using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Implementation.Accounts;

namespace SimpleFinance.Services.Tests.Implementation;

public class InstitutionServiceTests
{
    private readonly IInstitutionRepository _repository;
    private readonly InstitutionService _sut;

    public InstitutionServiceTests()
    {
        _repository = Substitute.For<IInstitutionRepository>();
        _sut = new InstitutionService(_repository);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldCallRepository()
    {
        var institution = new Institution(1, "Test Institution");
        _repository.AddInstitutionAsync(institution,CancellationToken.None).Returns(institution);

        await _sut.AddInstitutionAsync(institution,CancellationToken.None);

        await _repository.Received(1).AddInstitutionAsync(institution,CancellationToken.None);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldReturnInstitutionFromRepository()
    {
        var institution = new Institution(1, "Test Institution");
        _repository.AddInstitutionAsync(institution,CancellationToken.None).Returns(institution);

        var result = await _sut.AddInstitutionAsync(institution,CancellationToken.None);

        Assert.Equal(institution, result);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldPassCorrectInstitutionToRepository()
    {
        const int id = 1;
        const string name = "Test Institution";
        var institution = new Institution(id, name);
        _repository.AddInstitutionAsync(Arg.Any<Institution>(),CancellationToken.None).Returns(institution);

        await _sut.AddInstitutionAsync(institution,CancellationToken.None);

        await _repository.Received(1).AddInstitutionAsync(Arg.Is<Institution>(i => i.Id == id && i.Name == name),CancellationToken.None);
    }

    [Fact]
    public async Task AddInstitutionAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        var institution = new Institution(1, "Test");
        _repository.AddInstitutionAsync(institution,CancellationToken.None).Returns<Institution>(_ => throw new InvalidOperationException("Database error"));

        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.AddInstitutionAsync(institution,CancellationToken.None));
    }
}