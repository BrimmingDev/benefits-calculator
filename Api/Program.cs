using Api.Models;
using Api.Services;
using Api.Services.BenefitsCalcuationRules;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EmployeeBenefitsDatabaseSettings>(
    builder.Configuration.GetSection("EmployeeBenefitsDatabase"));

builder.Services.AddSingleton<EmployeesService>();

builder.Services.AddScoped<BenefitsCostCostCalculatorService>();
builder.Services.AddScoped<IBenefitsCostCalculationRule, BaseCostCalculationCostCalculationRule>();
builder.Services.AddScoped<IBenefitsCostCalculationRule, DependentsCostCalculationRule>();
builder.Services.AddScoped<IBenefitsCostCalculationRule, SalaryGreaterThanEightyThousandCalculationRule>();
builder.Services.AddScoped<IBenefitsCostCalculationRule, DependentAgeGreaterThanFiftyCalculationRule>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
