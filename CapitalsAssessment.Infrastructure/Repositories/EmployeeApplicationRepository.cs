using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Entities;
using CapitalsAssessment.Core.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CapitalsAssessment.Infrastructure.Repositories
{
    public class EmployeeApplicationRepository : IEmployeeApplicationRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        private readonly Container _applicationContainer;
 

        public EmployeeApplicationRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            var databaseName = _configuration["CosmosDb:DatabaseName"];
            var employeeEpplication = "EmployeeApplication";
            _applicationContainer = _cosmosClient.GetContainer(databaseName, employeeEpplication);
            var applicationProgram = "EmployeeProgram";
            _container = _cosmosClient.GetContainer(databaseName, applicationProgram);

        }
        public async Task<EmployeeApplicationDto> ApplyAsync(string programId, EmployeeApplicationDto employeeApplicationDto)
        {
            var response = new EmployeeApplicationDto();
            var result = await _container.ReadItemAsync<RecruitementProgram>(programId, new PartitionKey())
                ?? throw new Exception("No such program");
            var employeeProgram = result.Resource;
            if (employeeProgram is not null)
            {
                var application = new RecruitementApplication
                {
                    ProgramId = employeeProgram.Id,
                    EmployeeAnswers = employeeApplicationDto.EmployeeAnswers.Select(x => new EmployeeAnswer
                    {
                        QuestionId = x.QuestionId,
                        QuestionDescription = x.QuestionDescription,
                        Answer = x.Answer
                    }).ToList()
                };
                var addResult = await _applicationContainer.CreateItemAsync(application, new PartitionKey(application.Id));
                var entry = addResult.Resource;
                response = new EmployeeApplicationDto 
                { 
                    Id = entry.Id,
                    ProgramId = entry.ProgramId,
                    EmployeeAnswers = entry.EmployeeAnswers
                };
            }
            return response;
        }


        public async Task<EmployeeApplicationDto> UpdateApplication(EmployeeApplicationDto input)
        {

            var response = new EmployeeApplicationDto();
            if (string.IsNullOrEmpty(input?.Id))
                throw new Exception("ID fiend must not be empty");
            var result = await _applicationContainer.ReadItemAsync<RecruitementApplication>(input.Id, new PartitionKey(input.Id));
            var existingEntry = result.Resource;
            if (existingEntry is null)
                throw new Exception("Entry not found");
            existingEntry.EmployeeAnswers = input.EmployeeAnswers;
            var entry = await _applicationContainer
                .ReplaceItemAsync<RecruitementApplication>(existingEntry, input.Id, new PartitionKey(input.Id));
            var output = entry.Resource;
            return new EmployeeApplicationDto
            {
                Id = output.Id,
                ProgramId = output.ProgramId,
                EmployeeAnswers= output.EmployeeAnswers
            };

        }
    }
}
