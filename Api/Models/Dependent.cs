using Ardalis.GuardClauses;
using MongoDB.Bson;

namespace Api.Models;

public class Dependent : Person
{
    public string? Id { get; private set; }
    public Relationship Relationship { get; private set; }

    public Dependent(string firstName, string lastName, DateTime dateOfBirth, Relationship relationship)
        : base(firstName, lastName, dateOfBirth)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Relationship = relationship;
    }
}
