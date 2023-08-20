using Api.Exceptions;
using Ardalis.GuardClauses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public decimal Salary { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    [BsonElement("dependents")] 
    private List<Dependent> _dependents = new();
    public IReadOnlyCollection<Dependent> Dependents => _dependents.AsReadOnly();
    
    public Employee(string firstName, string lastName, DateTime dateOfBirth, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Salary = salary;
    }

    public void AddDependent(Dependent dependent)
    {
        Guard.Against.Null(dependent, nameof(dependent));
        if (dependent.Relationship is Relationship.Spouse &&
            _dependents.Exists(d => d.Relationship is Relationship.Spouse))
            throw new InvalidDependentException("Spouse or Domestic partner already exists for this employee");
        
        _dependents.Add(dependent);
    }
}
