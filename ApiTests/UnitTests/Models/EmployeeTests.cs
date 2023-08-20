using System;
using Api.Exceptions;
using Api.Models;
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
    public void AddDependentShouldThrowExceptionWhenAddingSpouseToEmployeeWithExistingSpouseDependent()
    {
        var employee = new Employee("Lebron", "James", new DateTime(1984, 12, 30), 75420.99m);
        var spouseOne = new Dependent("Spouse", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        var spouseTwo = new Dependent("Spouse2", "James", new DateTime(1984, 10, 30), Relationship.Spouse);
        employee.AddDependent(spouseOne);
    
        Action addDependent = () => employee.AddDependent(spouseTwo);

        addDependent.Should().Throw<InvalidDependentException>();
    }
    
}