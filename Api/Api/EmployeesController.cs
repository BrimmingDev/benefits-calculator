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
    
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}/paystub/{paystubId}")]
    public async Task<ActionResult<ApiResponse<GetPaystubDTO>>> GetEmployeePaystub(string id, int paystubId)
    {
        var employee = await _employeesService.GetAsync(id);

        if (employee is null) return NotFound();

        var paystub = employee.PayStubs.FirstOrDefault(p => p.Id == paystubId);

        if (paystub is null) return NotFound();
        
        return new ApiResponse<GetPaystubDTO>
        {
            Data = GetPaystubDTO.FromPaystub(paystub),
            Success = true
        };
    }
    
    [SwaggerOperation(Summary = "Create new employee")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<GetEmployeeDTO>>> Post(PostEmployeeDTO employeeDto)
    {
        var newEmployee = new Employee(employeeDto.FirstName, employeeDto.LastName, employeeDto.DateOfBirth,
            employeeDto.Salary, employeeDto.Dependents.ConvertAll(d =>
                new Dependent(d.FirstName, d.LastName, d.DateOfBirth, (Relationship)d.RelationshipId)));
        
        // Ideally this would be updated using some kind of event driven architecture but felt this was out of scope
        var monthlyBenefitsCost = _benefitsCostCostCalculatorService.CalculateBenefitsCost(newEmployee);
        newEmployee.UpdateBenefitCosts(monthlyBenefitsCost * 12);
        
        await _employeesService.CreateAsync(newEmployee);

        return new ApiResponse<GetEmployeeDTO>
        {
            Data = GetEmployeeDTO.FromEmployee(newEmployee),
            Success = true
        };
    }
    
    [SwaggerOperation(Summary = "Generate Paycheck for employee")]
    [HttpPost("{id}/GeneratePaycheck")]
    public async Task<ActionResult<ApiResponse<int>>> Generate(string id)
    {
        var employee = await _employeesService.GetAsync(id);

        if (employee is null) return NotFound();
        
        employee.GeneratePaystub();

        await _employeesService.UpdateAsync(id, employee);

        var paystubId = employee.PayStubs.Max(p => p.Id);

        return new ApiResponse<int>
        {
            Data = paystubId,
            Success = true
        };
    }
}
