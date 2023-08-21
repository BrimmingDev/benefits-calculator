using Api.Models;

namespace Api.Services.BenefitsCalcuationRules;

public class BaseCostCalculationCostCalculationRule : IBenefitsCostCalculationRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return 1000 + currentCost;
    }
}