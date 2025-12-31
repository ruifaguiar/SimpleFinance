using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Transaction;

namespace SimpleFinance.WebApi.Endpoints.Transactions.Handlers;

public class GetTransactionByIdHandler(ITransactionService transactionService)
{
    public async Task<IResult> HandleAsync(int transactionId, CancellationToken cancellationToken)
    {
        Domain.DomainObjects.Transaction transaction;

        try
        {
            transaction = await transactionService.GetTransactionByIdAsync(transactionId, cancellationToken);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.Ok(transaction.ToModel());
    }
}

