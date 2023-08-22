using Api.Models;

namespace Api.Services.BenefitsCalcuationRules;

public interface IBenefitsCostCalculationRule
{
    decimal CalculateCost(Employee employee, decimal currentCost);
}