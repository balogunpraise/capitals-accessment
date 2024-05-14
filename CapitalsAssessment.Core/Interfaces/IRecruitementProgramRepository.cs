using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Entities;

namespace CapitalsAssessment.Core.Interfaces
{
    public interface IRecruitementProgramRepository
    {
        Task<RecruitementProgram> CreateNewOrUpdateProgram(CreateProgramDto programDto);
        Task<CreateProgramDto> GetProgram(string id);

    }
}
