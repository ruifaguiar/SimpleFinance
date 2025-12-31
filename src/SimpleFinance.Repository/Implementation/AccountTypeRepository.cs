using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.AccountType;

namespace SimpleFinance.Repository.Implementation;

public class AccountTypeRepository(SimpleFinanceDbContext dbContext) : IAccountTypeRepository
{
    public async Task<AccountType> AddAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        var dbAccountType = accountType.ToDb();

        dbContext.AccountTypes.Add(dbAccountType);
        await dbContext.SaveChangesAsync(cancellationToken);

        return dbAccountType.ToDomain();
    }

    public async Task<IEnumerable<AccountType>> GetAllAccountTypesAsync(CancellationToken cancellationToken)
    {
        var accountTypes = await dbContext.AccountTypes.ToListAsync(cancellationToken);
        return accountTypes.Select(a => a.ToDomain());
    }

    public async Task<AccountType> GetAccountTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var accountType = await dbContext.AccountTypes.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(accountType, $"AccountType with ID {id} not found.");
        return accountType.ToDomain();
    }

    public async Task<AccountType> UpdateAccountTypeAsync(AccountType accountType, CancellationToken cancellationToken)
    {
        var dbAccountType = await dbContext.AccountTypes.FindAsync([accountType.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbAccountType, $"AccountType with ID {accountType.Id} not found.");

        dbAccountType.Name = accountType.Name;
        dbAccountType.Description = accountType.Description;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"AccountType with ID {accountType.Id} was modified by another process.", ex);
        }

        return dbAccountType.ToDomain();
    }

    public async Task<bool> DeleteAccountTypeAsync(int id, CancellationToken cancellationToken)
    {
        var dbAccountType = await dbContext.AccountTypes.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbAccountType, $"AccountType with ID {id} not found.");

        dbContext.AccountTypes.Remove(dbAccountType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

