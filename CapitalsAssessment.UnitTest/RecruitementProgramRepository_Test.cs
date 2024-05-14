using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CapitalsAssessment.UnitTest
{
    public class RecruitementProgramRepository_Test
    {
        private readonly IConfiguration _config = Mock.Of<IConfiguration>();
        private readonly CosmosClient _client = Mock.Of<CosmosClient>();


        [Fact]
        public void ProgramShouldThrowExceptionWhenQuestionIsZero()
        {
            //Arrange
            var input = new CreateProgramDto
            {
                ProgramTitle = "Test Program",
                ProgramDescription = "Test Program Description",
                Questions = new List<CustomQuestionDto>()
            };
            var handler = new RecruitementProgramRepository(_client, _config);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await handler.CreateNewOrUpdateProgram(input));
        }
    }
}
