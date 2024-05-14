using CapitalsAssessment.Core.Interfaces;
using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CapitalsAssessment.Infrastructure.Repositories
{
    public class RecruitementProgramRepository : IRecruitementProgramRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        public RecruitementProgramRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            var databaseName = _configuration["CosmosDbSettings:DatabaseName"];
            var applicationProgram = "EmployeeProgram";
            _container = _cosmosClient.GetContainer(databaseName, applicationProgram);
        }
        public async Task<RecruitementProgram> CreateNewOrUpdateProgram(CreateProgramDto programDto)
        {
            try
            {
                if (programDto.Questions == null || programDto.Questions.Count == 0)
                    throw new ArgumentException("Program must contain at least one question");
                var employeeProgram = new RecruitementProgram();
                if (!string.IsNullOrEmpty(programDto.Id))
                {
                    var result = await _container.ReadItemAsync<RecruitementProgram>(programDto.Id, new PartitionKey(programDto.Id))
                        ?? throw new Exception("program does not exist");
                    var existingItem = result.Resource;
                    if (existingItem != null)
                    {
                        existingItem.ProgramTitle = programDto.ProgramTitle;
                        existingItem.ProgramDescription = programDto.ProgramDescription;
                        existingItem.Questions = programDto?.Questions?.Select(x => new CustomQuestion
                        {
                            QuestionDescription = x.QuesttionDescription,
                            FieldType = x.FieldType,
                            Answer = x.Answer,
                            IsHidden = x.IsHidden,
                            IsInternal = x.IsInternal,
                            IsDropDown = x.IsDropDown,
                            IsRequired = x.IsRequired,
                            DropdownOptions = x.DropdownOptions?.Select(y => new Options
                            {
                                QuestionId = y.QuestionId,
                                OptionName = y.OptionName
                            }).ToList()
                        }).ToList();

                        var updateResult = await _container
                            .ReplaceItemAsync(existingItem, programDto.Id, new PartitionKey(programDto.Id));
                        employeeProgram = updateResult.Resource;
                    }
                }
                else
                {
                    var program = new RecruitementProgram
                    {
                        ProgramTitle = programDto.ProgramTitle,
                        ProgramDescription = programDto.ProgramDescription,
                        Questions = programDto?.Questions?.Select(x => new CustomQuestion
                        {
                            QuestionDescription = x.QuesttionDescription,
                            FieldType = x.FieldType,
                            Answer = x.Answer,
                            IsHidden = x.IsHidden,
                            IsInternal = x.IsInternal,
                            IsDropDown = x.IsDropDown,
                            IsRequired = x.IsRequired,
                            DropdownOptions = x.DropdownOptions?.Select(y => new Options
                            {
                                QuestionId = y.QuestionId,
                                OptionName = y.OptionName
                            }).ToList()
                        }).ToList()
                    };
                    var response = await _container.CreateItemAsync(program, new PartitionKey(program.Id));
                    employeeProgram = response.Resource;
                }
                return employeeProgram;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<CreateProgramDto> GetProgram(string id)
        {

            var response = await _container.ReadItemAsync<RecruitementProgram>(id, new PartitionKey(id));
            var resource = response.Resource;
            return new CreateProgramDto
            {
                Id = resource.Id,
                ProgramTitle = resource.ProgramTitle,
                ProgramDescription = resource.ProgramDescription,
                Questions = resource?.Questions?.Select(x => new CustomQuestionDto
                {
                    Id = x.Id,
                    ProgramId = x.ProgramId,
                    QuesttionDescription = x.QuestionDescription,
                    FieldType = x.FieldType,
                    IsRequired = x.IsRequired,
                    IsInternal = x.IsInternal,
                    IsDropDown= x.IsDropDown,
                    IsHidden = x.IsHidden,
                    DropdownOptions = x.DropdownOptions?.Select(y => new OptionsDto
                    {
                        QuestionId = y.QuestionId,
                        OptionName = y.OptionName
                    }).ToList()
                }).ToList()
            };
        }
    }
}
