using Api.ApiModels;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeesService _employeesService;
    private readonly BenefitsCostCostCalculatorService _benefitsCostCostCalculatorService;

    public EmployeesController(EmployeesService employeesService, 
        BenefitsCostCostCalculatorService benefitsCostCostCalculatorService)
    {
        _employeesService = employeesService;
        _benefitsCostCostCalculatorService = benefitsCostCostCalculatorService;
    } 
    
    

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDTO>>>> GetAll()
    {
        var employees = await _employeesService.GetAsync();

        var result = new ApiResponse<List<GetEmployeeDTO>>
        {
            Data = employees.Select(GetEmployeeDTO.FromEmployee).ToList(),
            Success = true
        };

        return result;
    }
    
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDTO>>> Get(string id)
    {
        var employee = await _employeesService.GetAsync(id);

        if (employee is null) return NotFound();

        return new ApiResponse<GetEmployeeDTO>
        {
            Data = GetEmployeeDTO.FromEmployee(employee),
            Success = true
        };
    }
    
    [SwaggerOperation(Summary = "Create new employee")]
    [HttpPost]
    public async Task<IActionResult> Post(PostEmployeeDTO employeeDto)
    {
        var newEmployee = new Employee(employeeDto.FirstName, employeeDto.LastName, employeeDto.DateOfBirth,
            employeeDto.Salary);
        newEmployee.AddDependent(new Dependent("Spouse", "James", DateTime.Now, Relationship.Spouse));
        
        await _employeesService.CreateAsync(newEmployee);

        return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
    }
}
