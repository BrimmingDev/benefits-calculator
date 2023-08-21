using Api.Services;
using Ardalis.GuardClauses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class Employee : Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public decimal Salary { get; private set; }
    public decimal BenefitsCost { get; private set; }
    
    [BsonElement("dependents")] 
    private List<Dependent> _dependents = new();
    public IReadOnlyCollection<Dependent> Dependents => _dependents.AsReadOnly();
    
    public Employee(string firstName, string lastName, DateTime dateOfBirth, decimal salary)
        : base(firstName, lastName, dateOfBirth)
    {
        Salary = Guard.Against.NegativeOrZero(salary, nameof(salary));
    }

    public void AddDependent(Dependent dependent)
    {
        Guard.Against.Null(dependent, nameof(dependent));
        if (dependent.Relationship is Relationship.Spouse or Relationship.DomesticPartner &&
            _dependents.Exists(d =>
                d.Relationship is Relationship.Spouse or Relationship.DomesticPartner))
            throw new ArgumentException("Spouse or Domestic partner already exists for this employee");
        
        _dependents.Add(dependent);
    }
}