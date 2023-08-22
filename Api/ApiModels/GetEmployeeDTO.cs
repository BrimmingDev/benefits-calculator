
namespace Api.ApiModels;

public class GetEmployeeDTO
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDTO> Dependents { get; set; } = new List<GetDependentDTO>();

    public static GetEmployeeDTO FromEmployee(Models.Employee employee)
    {
        return new GetEmployeeDTO()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = new List<GetDependentDTO>(employee.Dependents.Select(GetDependentDTO.FromDependent))
        };
    }
}
