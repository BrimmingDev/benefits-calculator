using Api.Models;

namespace Api.Services.BenefitsCalcuationRules;

public class DependentAgeGreaterThanFiftyCalculationRule : IBenefitsCostCalculationRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        if (employee.Dependents.Count == 0) return currentCost;

        return currentCost + 
               employee.Dependents.Count(d => d.DateOfBirth.Date.AddYears(50) <= DateTime.Now.Date) * 200m;
    }
}