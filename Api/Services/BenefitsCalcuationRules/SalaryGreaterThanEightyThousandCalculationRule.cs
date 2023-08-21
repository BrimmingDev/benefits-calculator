using Api.Models;

namespace Api.Services.BenefitsCalcuationRules;

public class SalaryGreaterThanEightyThousandCalculationRule : IBenefitsCostCalculationRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return employee.Salary >= 80000m
            ? currentCost + Math.Round(employee.Salary * .02m, 2, MidpointRounding.ToPositiveInfinity)
            : currentCost;
    }
}