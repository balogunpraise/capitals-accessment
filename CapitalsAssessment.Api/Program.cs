using CapitalsAssessment.Core.Interfaces;
using CapitalsAssessment.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeApplicationRepository, EmployeeApplicationRepository>();
builder.Services.AddScoped<IRecruitementProgramRepository, RecruitementProgramRepository>();
var configuration = builder.Configuration;
builder.Services.AddSingleton((provider) =>
{
    var endpointUri = configuration["CosmosDbSettings:EndpointUri"];
    var primaryKey = configuration["CosmosDbSettings:PrimaryKey"];
    var databaseName = configuration["CosmosDbSettings:DatabaseName"];

    var cosmosClientOpions = new CosmosClientOptions
    {
        ApplicationName = databaseName,
    };

    var loggerFactory = LoggerFactory.Create(option =>
    {
        option.AddConsole();
    });
    var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOpions);
    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
    return cosmosClient;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
