using System;
using Api.Models;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests.Models;

public class PersonTests
{
    [Fact]
    public void NewPersonShouldThrowArgumentExceptionWhenGivenEmptyFirstname()
    {
        Action createPerson = () => new MockPerson("", "user", DateTime.Now);

        createPerson.Should().Throw<ArgumentException>().WithParameterName("firstName");
    }
    
    [Fact]
    public void NewPersonShouldThrowArgumentExceptionWhenGivenWhitespaceFirstname()
    {
        Action createPerson = () => new MockPerson("   ", "user", DateTime.Now);

        createPerson.Should().Throw<ArgumentException>().WithParameterName("firstName");
    }
    
    [Fact]
    public void NewPersonShouldThrowArgumentExceptionWhenGivenEmptyLastname()
    {
        Action createPerson = () => new MockPerson("test", "", DateTime.Now);

        createPerson.Should().Throw<ArgumentException>().WithParameterName("lastName");
    }
    
    [Fact]
    public void NewPersonShouldThrowArgumentExceptionWhenGivenWhitespaceLastname()
    {
        Action createPerson = () => new MockPerson("test", "  ", DateTime.Now);

        createPerson.Should().Throw<ArgumentException>().WithParameterName("lastName");
    }
    
    [Fact]
    public void NewPersonShouldThrowArgumentExceptionWhenGivenDateOfBirthGreaterThanToday()
    {
        Action createPerson = () => new MockPerson("test", "user", DateTime.Now.AddDays(1));

        createPerson.Should().Throw<ArgumentException>()
            .WithMessage($"{nameof(MockPerson.DateOfBirth)} cannot be greater than Today's date");
    }

    [Fact]
    public void NewPersonShouldReturnAProperlyCreatedPerson()
    {
        var firstName = "test";
        var lastName = "user";
        var dateOfBirth = DateTime.Now;

        var person = new MockPerson(firstName, lastName, dateOfBirth);

        person.FirstName.Should().Be(firstName);
        person.LastName.Should().Be(lastName);
        person.DateOfBirth.Should().Be(dateOfBirth);
    }
}

public class MockPerson : Person
{
    public MockPerson(string firstName, string lastName, DateTime dateOfBirth) : base(firstName, lastName, dateOfBirth)
    {
    }
}