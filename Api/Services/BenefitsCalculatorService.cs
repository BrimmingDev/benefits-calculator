using Api.Models;

namespace Api.Services;


public interface IBenefitsCostRule
{
    decimal CalculateCost(Employee employee, decimal currentCost);
}

public class BaseCostCostRule : IBenefitsCostRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return 1000 + currentCost;
    }
}

public class DependentsCostRule : IBenefitsCostRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return currentCost + 600m * employee.Dependents.Count;
    }
}

public class SalaryGreaterThanEightyThousandRule : IBenefitsCostRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        return employee.Salary >= 80000m
            ? currentCost + Math.Round(employee.Salary * .02m, 2, MidpointRounding.ToPositiveInfinity)
            : currentCost;
    }
}

public class DependentAgeGreaterThanFiftyRule : IBenefitsCostRule
{
    public decimal CalculateCost(Employee employee, decimal currentCost)
    {
        if (employee.Dependents.Count == 0) return currentCost;

        return currentCost + 
               employee.Dependents.Count(d => d.DateOfBirth.Date.AddYears(50) <= DateTime.Now.Date) * 200m;
    }
}

public class BenefitsCalculatorService
{
    private readonly List<IBenefitsCostRule> _rules = new();

    public BenefitsCalculatorService(IEnumerable<IBenefitsCostRule> rules)
    {
        _rules.AddRange(rules);
    }

    public decimal CalculateBenefitsCost(Employee employee)
    {
        return _rules.Aggregate(0m, (current, rule) => rule.CalculateCost(employee, current));
    }
}
