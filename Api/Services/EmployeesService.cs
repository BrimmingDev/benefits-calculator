using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Services;

public class EmployeesService
{
    private readonly IMongoCollection<Employee> _employeesCollection;

    public EmployeesService(IOptions<EmployeeBenefitsDatabaseSettings> employeeBenefitsDatabaseSettings)
    {
        var mongoClient = new MongoClient(employeeBenefitsDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(employeeBenefitsDatabaseSettings.Value.DatabaseName);
        _employeesCollection =
            mongoDatabase.GetCollection<Employee>(employeeBenefitsDatabaseSettings.Value.EmployeesCollectionName);
    }

    public async Task<List<Employee>> GetAsync() => await _employeesCollection.Find(_ => true).ToListAsync();
    
    public async Task CreateAsync(Employee employee) => await _employeesCollection.InsertOneAsync(employee);
}