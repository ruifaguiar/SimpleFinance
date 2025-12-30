using System.Diagnostics.CodeAnalysis;

namespace SimpleFinance.Domain.Exceptions;

public class NotFoundException :Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNotFound([NotNull] object? entity, string message)
    {
        if (entity == null)
        {
            throw new NotFoundException(message);
        }
    }
}