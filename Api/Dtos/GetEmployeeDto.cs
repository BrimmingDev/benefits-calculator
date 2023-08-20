
namespace Api.Dtos;

public class GetEmployeeDto
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();

    public static GetEmployeeDto FromEmployee(Models.Employee employee)
    {
        return new GetEmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = new List<GetDependentDto>(employee.Dependents.Select(GetDependentDto.FromDependent))
        };
    }
}
