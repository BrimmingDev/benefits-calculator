using Api.Models;

namespace Api.Dtos;

public class GetDependentDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }

    public static GetDependentDto FromDependent(Models.Dependent dependent)
    {
        return new GetDependentDto()
        {
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };
    }
}
