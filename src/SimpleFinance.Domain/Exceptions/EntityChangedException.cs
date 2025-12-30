namespace SimpleFinance.Domain.Exceptions;

public class EntityChangedException(string message, Exception innerException) : Exception(message, innerException);