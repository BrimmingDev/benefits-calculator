using Ardalis.GuardClauses;

namespace Api.Models;

public class Dependent : Person
{
    public Relationship Relationship { get; private set; }

    public Dependent(string firstName, string lastName, DateTime dateOfBirth, Relationship relationship)
        : base(firstName, lastName, dateOfBirth)
    {
        Relationship = relationship;
    }
}
