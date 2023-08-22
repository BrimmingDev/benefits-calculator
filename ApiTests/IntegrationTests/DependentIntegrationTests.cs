using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.ApiModels;
using Api.Models;
using Ardalis.HttpClientTestExtensions;
using FluentAssertions;
using MongoDB.Bson;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var dependents = new List<GetDependentDTO>
        {
            new()
            {
                Id = "64e3f9b52901660006e20c96",
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2).ToUniversalTime()
            },
            new()
            {
                Id = "64e3f8fb2901660006e20c92",
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3).ToUniversalTime()
            },
            new()
            {
                Id = "64e3f8fb2901660006e20c93",
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23).ToUniversalTime()
            },
            new()
            {
                Id = "64e3f8fb2901660006e20c94",
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18).ToUniversalTime()
            }
        };

        var response =
            await HttpClient.GetAndDeserializeAsync<ApiResponse<List<GetDependentDTO>>>("/api/v1/dependents");

        response.Success.Should().BeTrue();
        response.Data.Should().NotBeEmpty();
        response.Data.Count.Should().Be(4);
        response.Data.First(d => d.Id == "64e3f8fb2901660006e20c93").Should()
            .BeEquivalentTo(dependents.First(d => d.Id == "64e3f8fb2901660006e20c93"));
    }
    
    [Fact]
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var dependent = new GetDependentDTO()
        {
            Id = "64e3f8fb2901660006e20c92",
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3).ToUniversalTime()
        };

        var response =
            await HttpClient.GetAndDeserializeAsync<ApiResponse<GetDependentDTO>>(
                "/api/v1/dependents/64e3f8fb2901660006e20c92");

        response.Success.Should().BeTrue();
        response.Data.Should().BeEquivalentTo(dependent);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{ObjectId.GenerateNewId().ToString()}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

