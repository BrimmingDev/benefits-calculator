using Api.Services;
using Ardalis.GuardClauses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;

namespace Api.Models;

public class Employee : Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public decimal Salary { get; private set; }
    private decimal _grossBiWeeklySalary => Salary / 26;
    public decimal BenefitsCost { get; private set; } = 0m;
    private decimal _biWeeklyBenefitsCost => BenefitsCost / 26;
    
    [BsonElement("dependents")] 
    private List<Dependent> _dependents = new();
    public IReadOnlyCollection<Dependent> Dependents => _dependents.AsReadOnly();

    [BsonElement("paystubs")] 
    private List<PayStub> _payStubs = new();
    public IReadOnlyCollection<PayStub> PayStubs => _payStubs.AsReadOnly();
    
    public Employee(string firstName, string lastName, DateTime dateOfBirth, decimal salary, 
        List<Dependent>? dependents = null)
        : base(firstName, lastName, dateOfBirth)
    {
        Salary = Guard.Against.NegativeOrZero(salary, nameof(salary));

        dependents?.ForEach(AddDependent);
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

    public void UpdateBenefitCosts(decimal cost)
    {
        BenefitsCost = Guard.Against.Negative(cost, nameof(cost));
    }

    public void GeneratePaystub()
    {
        _payStubs ??= new();
        
        var grossPay = 0m;
        var benefitsCost = 0m;
        var payperiod = _payStubs.Count + 1;
        
        if (payperiod % 26 == 0)
        {
            grossPay = Salary - _payStubs.Where(p => p.Id >= payperiod - 25).Sum(p => p.Gross);
            benefitsCost = BenefitsCost - _payStubs.Where(p => p.Id >= payperiod - 25).Sum(p => p.BenefitsCost);
        }
        else
        {
            var payRemainder = _payStubs.Count == 0
                ? 0
                : _grossBiWeeklySalary - _payStubs.First(p => p.Id == payperiod - 1).Gross;
            grossPay = Math.Round(payRemainder + _grossBiWeeklySalary, 2, MidpointRounding.AwayFromZero);
            
            var benefitsRemainder = _payStubs.Count == 0
                ? 0
                : _biWeeklyBenefitsCost - _payStubs.First(p => p.Id == payperiod - 1).BenefitsCost;
            benefitsCost = Math.Round(benefitsRemainder + _biWeeklyBenefitsCost, 2, MidpointRounding.AwayFromZero);
        }
        
        _payStubs.Add(new PayStub(payperiod, grossPay, benefitsCost, grossPay - benefitsCost));
    }
}