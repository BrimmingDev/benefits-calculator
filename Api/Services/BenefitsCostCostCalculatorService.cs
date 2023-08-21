using Api.Models;
using Api.Services.BenefitsCalcuationRules;

namespace Api.Services;

public class BenefitsCostCostCalculatorService
{
    private readonly List<IBenefitsCostCalculationRule> _rules = new();

    public BenefitsCostCostCalculatorService(IEnumerable<IBenefitsCostCalculationRule> rules)
    {
        _rules.AddRange(rules);
    }

    public decimal CalculateBenefitsCost(Employee employee)
    {
        return _rules.Aggregate(0m, (current, rule) => rule.CalculateCost(employee, current));
    }
}