using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Implementation;

namespace SimpleFinance.Repository.Tests.Implementation;

public class InstitutionRepositoryTests : IDisposable
{
    private readonly SimpleFinanceDbContext dbContext;
    private readonly InstitutionRepository repository;

    public InstitutionRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SimpleFinanceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        dbContext = new SimpleFinanceDbContext(options);
        repository = new InstitutionRepository(dbContext);
    }

    public void Dispose() => dbContext.Dispose();

    #region AddInstitutionAsync Tests

    [Fact]
    public async Task AddInstitutionAsync_WithValidInstitution_ShouldAddToDatabase()
    {
        var institution = new Institution(0, "Test Bank");

        await repository.AddInstitutionAsync(institution, CancellationToken.None);

        var dbInstitution = await dbContext.Institutions.FirstOrDefaultAsync();
        Assert.NotNull(dbInstitution);
        Assert.Equal("Test Bank", dbInstitution.Name);
    }

    [Fact]
    public async Task AddInstitutionAsync_WithValidInstitution_ShouldReturnInstitution()
    {
        var institution = new Institution(0, "Test Bank");

        var result = await repository.AddInstitutionAsync(institution, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Test Bank", result.Name);
    }

    [Fact]
    public async Task AddInstitutionAsync_ShouldSetCreatedAt()
    {
        var institution = new Institution(0, "Test Bank");
        var beforeAdd = DateTime.UtcNow;

        await repository.AddInstitutionAsync(institution, CancellationToken.None);

        var dbInstitution = await dbContext.Institutions.FirstOrDefaultAsync();
        Assert.NotNull(dbInstitution);
        Assert.NotNull(dbInstitution.CreatedAt);
        Assert.True(dbInstitution.CreatedAt >= beforeAdd);
    }

    #endregion

    #region GetAllInstitutionsAsync Tests

    [Fact]
    public async Task GetAllInstitutionsAsync_WithNoInstitutions_ShouldReturnEmptyList()
    {
        var result = await repository.GetAllInstitutionsAsync(CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllInstitutionsAsync_WithInstitutions_ShouldReturnAllInstitutions()
    {
        dbContext.Institutions.AddRange(
            new Database.Entities.Institution { Id = 1, Name = "Bank A" },
            new Database.Entities.Institution { Id = 2, Name = "Bank B" },
            new Database.Entities.Institution { Id = 3, Name = "Bank C" }
        );
        await dbContext.SaveChangesAsync();

        var result = await repository.GetAllInstitutionsAsync(CancellationToken.None);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAllInstitutionsAsync_ShouldReturnDomainObjects()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Test Bank" });
        await dbContext.SaveChangesAsync();

        var result = await repository.GetAllInstitutionsAsync(CancellationToken.None);

        Assert.All(result, i => Assert.IsType<Institution>(i));
    }

    #endregion

    #region GetInstitutionByIdAsync Tests

    [Fact]
    public async Task GetInstitutionByIdAsync_WithExistingId_ShouldReturnInstitution()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Test Bank" });
        await dbContext.SaveChangesAsync();

        var result = await repository.GetInstitutionByIdAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Bank", result.Name);
    }

    [Fact]
    public async Task GetInstitutionByIdAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => repository.GetInstitutionByIdAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task GetInstitutionByIdAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => repository.GetInstitutionByIdAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion

    #region UpdateInstitutionAsync Tests

    [Fact]
    public async Task UpdateInstitutionAsync_WithExistingInstitution_ShouldUpdateName()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Old Name" });
        await dbContext.SaveChangesAsync();
        var updatedInstitution = new Institution(1, "New Name");

        await repository.UpdateInstitutionAsync(updatedInstitution, CancellationToken.None);

        var dbInstitution = await dbContext.Institutions.FindAsync(1);
        Assert.Equal("New Name", dbInstitution!.Name);
    }

    [Fact]
    public async Task UpdateInstitutionAsync_WithExistingInstitution_ShouldReturnUpdatedInstitution()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Old Name" });
        await dbContext.SaveChangesAsync();
        var updatedInstitution = new Institution(1, "New Name");

        var result = await repository.UpdateInstitutionAsync(updatedInstitution, CancellationToken.None);

        Assert.Equal("New Name", result.Name);
    }

    [Fact]
    public async Task UpdateInstitutionAsync_ShouldSetModifiedAt()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Old Name" });
        await dbContext.SaveChangesAsync();
        var updatedInstitution = new Institution(1, "New Name");
        var beforeUpdate = DateTime.UtcNow;

        await repository.UpdateInstitutionAsync(updatedInstitution, CancellationToken.None);

        var dbInstitution = await dbContext.Institutions.FindAsync(1);
        Assert.NotNull(dbInstitution!.ModifiedAt);
        Assert.True(dbInstitution.ModifiedAt >= beforeUpdate);
    }

    [Fact]
    public async Task UpdateInstitutionAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        var institution = new Institution(999, "Test");

        await Assert.ThrowsAsync<NotFoundException>(
            () => repository.UpdateInstitutionAsync(institution, CancellationToken.None));
    }

    #endregion

    #region DeleteInstitutionAsync Tests

    [Fact]
    public async Task DeleteInstitutionAsync_WithExistingId_ShouldRemoveFromDatabase()
    {
        dbContext.Institutions.Add(new Database.Entities.Institution { Id = 1, Name = "Test Bank" });
        await dbContext.SaveChangesAsync();

        await repository.DeleteInstitutionAsync(1, CancellationToken.None);

        var dbInstitution = await dbContext.Institutions.FindAsync(1);
        Assert.Null(dbInstitution);
    }

    [Fact]
    public async Task DeleteInstitutionAsync_WithNonExistingId_ShouldThrowNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(
            () => repository.DeleteInstitutionAsync(999, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteInstitutionAsync_WithNonExistingId_ShouldContainIdInMessage()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => repository.DeleteInstitutionAsync(999, CancellationToken.None));

        Assert.Contains("999", exception.Message);
    }

    #endregion
}