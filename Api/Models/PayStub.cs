using Ardalis.GuardClauses;

namespace Api.Models;

public class PayStub
{
    public int Id { get; private set; }
    public decimal Gross { get; private set; }
    public decimal BenefitsCost { get; private set; }
    public decimal Net { get; private set; }

    public PayStub(int payperiod, decimal gross, decimal benefitsCost, decimal net)
    {
        Id = payperiod;
        Gross = Guard.Against.Negative(gross, nameof(gross));
        BenefitsCost = Guard.Against.Negative(benefitsCost, nameof(benefitsCost));
        Net = Guard.Against.Negative(net, nameof(net));
    }
}