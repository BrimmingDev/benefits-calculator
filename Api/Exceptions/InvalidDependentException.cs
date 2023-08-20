namespace Api.Exceptions;

public class InvalidDependentException : Exception
{
    public InvalidDependentException(string message) : base(message) {}
}