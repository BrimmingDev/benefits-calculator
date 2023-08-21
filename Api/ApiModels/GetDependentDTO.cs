using Api.Models;

namespace Api.ApiModels;

public class GetDependentDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }

    public static GetDependentDTO FromDependent(Models.Dependent dependent)
    {
        return new GetDependentDTO()
        {
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };
    }
}
