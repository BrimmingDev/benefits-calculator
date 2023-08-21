using Ardalis.GuardClauses;

namespace Api.Models;

public abstract class Person
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    protected Person(string firstName, string lastName, DateTime dateOfBirth)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        DateOfBirth = Guard.Against.AgainstExpression(d => d.Date <= DateTime.Now.Date, dateOfBirth,
            $"{nameof(DateOfBirth)} cannot be greater than Today's date");
    }
}