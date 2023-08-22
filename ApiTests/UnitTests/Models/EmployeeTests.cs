using System;
using System.Collections.Generic;
using Api.Models;
using Api.Services;
using Api.Services.BenefitsCalcuationRules;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests.Models;

public class EmployeeTests
{
    [Fact]
    public void AddDependentShouldThrowArgumentNullExceptionGivenNullDependent()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);

        Action addDependent = () => employee.AddDependent(null);

        addDependent.Should().Throw<ArgumentNullException>().WithParameterName("dependent");
    }
    
    [Fact]
    public void AddDependentShouldThrowExceptionWhenAddingSpouseToEmployeeWithExistingSpouse()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var spouseOne = new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        var spouseTwo = new Dependent("Spouse2", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        employee.AddDependent(spouseOne);
    
        Action addDependent = () => employee.AddDependent(spouseTwo);

        addDependent.Should().Throw<ArgumentException>()
            .WithMessage("Spouse or Domestic partner already exists for this employee");
    }
    
    [Fact]
    public void AddDependentShouldThrowExceptionWhenAddingSpouseToEmployeeWithExistingDomesticPartner()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var domesticPartner = 
            new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.DomesticPartner);
        var spouse = new Dependent("Spouse2", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        employee.AddDependent(domesticPartner);
    
        Action addDependent = () => employee.AddDependent(spouse);

        addDependent.Should().Throw<ArgumentException>()
            .WithMessage("Spouse or Domestic partner already exists for this employee");
    }
    
    [Fact]
    public void AddDependentShouldThrowExceptionWhenAddingDomesticPartnerToEmployeeWithExistingSpouse()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var spouse = new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        var domesticPartner = 
            new Dependent("Spouse2", "James", new DateTime(1984, 10, 30), Relationship.DomesticPartner);
        employee.AddDependent(spouse);
    
        Action addDependent = () => employee.AddDependent(domesticPartner);

        addDependent.Should().Throw<ArgumentException>()
            .WithMessage("Spouse or Domestic partner already exists for this employee");
    }
    
    [Fact]
    public void AddDependentShouldThrowExceptionWhenAddingDomesticPartnerToEmployeeWithExistingDomesticPartner()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var domesticPartnerOne = 
            new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.DomesticPartner);
        var domesticPartnerTwo = 
            new Dependent("Spouse2", "James", new DateTime(1984, 10, 30), Relationship.DomesticPartner);
        employee.AddDependent(domesticPartnerOne);
    
        Action addDependent = () => employee.AddDependent(domesticPartnerTwo);

        addDependent.Should().Throw<ArgumentException>()
            .WithMessage("Spouse or Domestic partner already exists for this employee");
    }
}