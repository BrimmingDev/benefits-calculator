using Api.Models;

namespace Api.Services.BenefitsCalcuationRules;

public class DependentsCostCalculationRule : IBenefitsCostCalculationRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return currentCost + 600m * employee.Dependents.Count;
    }
}