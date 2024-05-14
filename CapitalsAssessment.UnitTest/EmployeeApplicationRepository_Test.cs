using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Entities;
using CapitalsAssessment.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CapitalsAssessment.UnitTest
{
    public class EmployeeApplicationRepository_Test
    {
        private readonly IConfiguration _config = Mock.Of<IConfiguration>();
        private readonly CosmosClient _client = Mock.Of<CosmosClient>();

        [Fact]
        public void ApplyAsync_Should_Throw_Exception_For_Empty_Program_Id()
        {
            //Arrange
            var input = new EmployeeApplicationDto
            {
                ProgramId = null,
                EmployeeAnswers =
                [
                    new()
                    {
                        QuestionDescription = "First Name",
                        Answer = "Test Name"
                    }
                ]
            };
            var handler = new EmployeeApplicationRepository(_client, _config);
  
            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await handler.ApplyAsync(input.ProgramId, input));
        }
    }
}