using NSubstitute;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Services.Implementation;

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
        var institution = new Institution(Guid.NewGuid(), "Test Institution");
        _repository.AddInstitutionAsync(institution).Returns(institution);

        await _sut.AddInstitutionAsync(institution);

        await _repository.Received(1).AddInstitutionAsync(institution);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldReturnInstitutionFromRepository()
    {
        var institution = new Institution(Guid.NewGuid(), "Test Institution");
        _repository.AddInstitutionAsync(institution).Returns(institution);

        var result = await _sut.AddInstitutionAsync(institution);

        Assert.Equal(institution, result);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldPassCorrectInstitutionToRepository()
    {
        var id = Guid.NewGuid();
        var name = "Test Institution";
        var institution = new Institution(id, name);
        _repository.AddInstitutionAsync(Arg.Any<Institution>()).Returns(institution);

        await _sut.AddInstitutionAsync(institution);

        await _repository.Received(1).AddInstitutionAsync(Arg.Is<Institution>(i => i.Id == id && i.Name == name));
    }

    [Fact]
    public async Task AddInstitutionAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        var institution = new Institution(Guid.NewGuid(), "Test");
        _repository.AddInstitutionAsync(institution).Returns<Institution>(_ => throw new InvalidOperationException("Database error"));

        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.AddInstitutionAsync(institution));
    }
}