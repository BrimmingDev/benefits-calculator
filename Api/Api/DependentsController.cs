using Api.ApiModels;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly EmployeesService _employeesService;
    
    public DependentsController(EmployeesService employeesService)
    {
        _employeesService = employeesService;
    }
    
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDTO>>> Get(string id)
    {
        var employee = await _employeesService.GetWithDependId(id);

        if (employee is null) return NotFound();

        var dependent = employee.Dependents.FirstOrDefault(d => d.Id == id);

        if (dependent is null) return NotFound();

        return new ApiResponse<GetDependentDTO>
        {
            Data = GetDependentDTO.FromDependent(dependent),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDTO>>>> GetAll()
    {
        var employees = await _employeesService.GetAsync();

        var dependents = employees.SelectMany(e => e.Dependents).Select(GetDependentDTO.FromDependent).ToList();

        return new ApiResponse<List<GetDependentDTO>>
        {
            Data = dependents,
            Success = true
        };
    }
}
