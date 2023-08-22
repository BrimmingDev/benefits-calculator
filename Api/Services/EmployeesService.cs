using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<Employee?> GetAsync(string id) => 
        await _employeesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Employee?> GetWithDependId(string dependentId)
    {
        var filter = Builders<Employee>.Filter.Lte("dependents._id", dependentId);

        return await _employeesCollection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task CreateAsync(Employee employee) => await _employeesCollection.InsertOneAsync(employee);

    public async Task UpdateAsync(string id, Employee updatedEmployee) =>
        await _employeesCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);
}