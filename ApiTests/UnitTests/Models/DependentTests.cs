using System;
using Api.Models;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests.Models;

public class DependentTests
{
    [Fact]
    public void NewDependentShouldReturnProperlyCreatedDependent()
    {
        var relationship = Relationship.Spouse;

        var dependent = new Dependent("test", "dependent", DateTime.Now, relationship);

        dependent.Relationship.Should().Be(relationship);
    }
}