namespace Api.ApiModels;

public class PostEmployeeDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<PostDependentDTO> Dependents { get; set; } = new();
}