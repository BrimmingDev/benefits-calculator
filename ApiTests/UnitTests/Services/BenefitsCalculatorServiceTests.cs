using System;
using System.Collections.Generic;
using Api.Models;
using Api.Services;
using Api.Services.BenefitsCalcuationRules;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests.Services;

public class BenefitsCalculatorServiceTests
{
   
    [Fact]
    public void BaseLineEmployeeShouldReturnCostsOfOneThousand()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var sut = new BaseCostCalculationCostCalculationRule();

        var result = sut.CalculateCost(employee, 0m);

        result.Should().Be(1000m);
    }
    
    [Fact]
    public void EmployeeWithTwoDependentsReturnsCostsOfTwentyTwoHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        employee.AddDependent(new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.Spouse));
        employee.AddDependent(new Dependent("Child1", "James", new DateTime(2010, 6, 15), Relationship.Child));
        var sut = new DependentsCostCalculationRule();

        var result = sut.CalculateCost(employee, 0m);
    
        result.Should().Be(1200m);
    }
    
    [Fact]
    public void EmployeeWithSalaryOfEightyThousandReturnsTwentySixHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 80000m);
        var sut = new SalaryGreaterThanEightyThousandCalculationRule();

        var result = sut.CalculateCost(employee, 0m);
    
        result.Should().Be(employee.Salary * .02m);
    }
    
    [Fact]
    public void EmployeeWithDependentsOverFiftyReturnsEighteenHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        employee.AddDependent(new Dependent("Spouse", "James", DateTime.Now.AddYears(-50), Relationship.Spouse));
        var sut = new DependentAgeGreaterThanFiftyCalculationRule();

        var result = sut.CalculateCost(employee, 0m);
    
        result.Should().Be(200m);
    }
    
    [Fact]
    public void BenefitsCalculatorServiceWithMultipleRulesReturnsSumOfAllRules()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        employee.AddDependent(new Dependent("Spouse", "James", DateTime.Now.AddYears(-50), Relationship.Spouse));
        var rules = new List<IBenefitsCostCalculationRule>()
        {
            new BaseCostCalculationCostCalculationRule(),
            new DependentsCostCalculationRule(),
            new DependentAgeGreaterThanFiftyCalculationRule()
        };
        var sut = new BenefitsCostCostCalculatorService(rules);

        var result = sut.CalculateBenefitsCost(employee);
    
        result.Should().Be(1800m);
    }
}