using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.ApiModels;
using Api.Models;
using MongoDB.Bson;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDTO>
        {
            new()
            {
                Id = "64e3f7e22901660006e20c91",
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30).ToUniversalTime()
            },
            new()
            {
                Id = "64e3f8fb2901660006e20c95",
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10).ToUniversalTime(),
                Dependents = new List<GetDependentDTO>
                {
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
                }
            },
            new()
            {
                Id = "64e3f9b52901660006e20c97",
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17).ToUniversalTime(),
                Dependents = new List<GetDependentDTO>
                {
                    new()
                    {
                        Id = "64e3f9b52901660006e20c96",
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2).ToUniversalTime()
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }
    
    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/64e3f7e22901660006e20c91");
        var employee = new GetEmployeeDTO
        {
            Id = "64e3f7e22901660006e20c91",
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30).ToUniversalTime()
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{ObjectId.GenerateNewId().ToString()}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

