using Microsoft.EntityFrameworkCore;
using SimpleFinance.Database;
using SimpleFinance.Domain.DomainObjects;
using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Repository.Interfaces;
using SimpleFinance.Repository.Mappings.Account;

namespace SimpleFinance.Repository.Implementation;

public class AccountRepository(SimpleFinanceDbContext dbContext) : IAccountRepository
{
    public async Task<Account> AddAccountAsync(Account account, CancellationToken cancellationToken)
    {
        var dbAccount = account.ToDb();
        dbAccount.CreatedAt = DateTime.UtcNow;

        dbContext.Accounts.Add(dbAccount);
        await dbContext.SaveChangesAsync(cancellationToken);

        return dbAccount.ToDomain();
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Accounts.ToListAsync(cancellationToken);
        return accounts.Select(a => a.ToDomain());
    }

    public async Task<Account> GetAccountByIdAsync(int id, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(account, $"Account with ID {id} not found.");
        return account.ToDomain();
    }

    public async Task<Account> UpdateAccountAsync(Account account, CancellationToken cancellationToken)
    {
        var dbAccount = await dbContext.Accounts.FindAsync([account.Id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbAccount, $"Account with ID {account.Id} not found.");

        dbAccount.Name = account.Name;
        dbAccount.AccountNumber = account.AccountNumber;
        dbAccount.AccountTypeId = account.AccountTypeId;
        dbAccount.Balance = account.Balance;
        dbAccount.Currency = account.Currency;
        dbAccount.InstitutionId = account.InstitutionId;
        dbAccount.IsActive = account.IsActive;
        dbAccount.OpenedAt = account.OpenedAt;
        dbAccount.ClosedAt = account.ClosedAt;
        dbAccount.ModifiedAt = DateTime.UtcNow;

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityChangedException($"Account with ID {account.Id} was modified by another process.", ex);
        }

        return dbAccount.ToDomain();
    }

    public async Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken)
    {
        var dbAccount = await dbContext.Accounts.FindAsync([id], cancellationToken);
        NotFoundException.ThrowIfNotFound(dbAccount, $"Account with ID {id} not found.");

        dbContext.Accounts.Remove(dbAccount);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

