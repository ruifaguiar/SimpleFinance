using System.Diagnostics.CodeAnalysis;

namespace SimpleFinance.Domain.Exceptions;

public class InstitutionNotFoundException : Exception
{
    public InstitutionNotFoundException(Guid institutionId)
        : base($"Institution with ID {institutionId} was not found.")
    {
    }

    public InstitutionNotFoundException(Guid institutionId, string message)
        : base(message)
    {
    }

    public InstitutionNotFoundException(Guid institutionId, string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNotFound([NotNull] object? institution, Guid institutionId)
    {
        if (institution == null)
        {
            throw new InstitutionNotFoundException(institutionId, $"Institution with ID {institutionId} was not found.");
        }
    }
}