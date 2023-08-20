using System;
using Api.Models;
using Api.Services;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests.Services;

public class BenefitsCalculatorServiceTests
{
    private readonly BenefitsCalculatorService _calculatorService = new();
    
    [Fact]
    public void BaseLineEmployeeShouldReturnCostsOfOneThousand()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);

        var result = _calculatorService.CalculateBenefitsCost(employee);

        result.Should().Be(1000m);
    }
    
    [Fact]
    public void EmployeeWithTwoDependentsReturnsCostsOfTwentyTwoHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        employee.AddDependent(new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.Spouse));
        employee.AddDependent(new Dependent("Child1", "James", new DateTime(2010, 6, 15), Relationship.Child));

        var result = _calculatorService.CalculateBenefitsCost(employee);

        result.Should().Be(2200m);
    }
    
    [Fact]
    public void EmployeeWithSalaryOfEightyThousandReturnsTwentySixHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 80000m);

        var result = _calculatorService.CalculateBenefitsCost(employee);

        result.Should().Be(2600m);
    }
    
    [Fact]
    public void EmployeeWithDependentsOverFiftyReturnsEighteenHundred()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        employee.AddDependent(new Dependent("Spouse", "James", DateTime.Now.AddYears(-50), Relationship.Spouse));

        var result = _calculatorService.CalculateBenefitsCost(employee);

        result.Should().Be(1800m);
    }
}