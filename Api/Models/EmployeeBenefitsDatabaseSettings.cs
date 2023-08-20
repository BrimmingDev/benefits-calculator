namespace Api.Models;

public class EmployeeBenefitsDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string EmployeesCollectionName { get; set; } = null!;
}