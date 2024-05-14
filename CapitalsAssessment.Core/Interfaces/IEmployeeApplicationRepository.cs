using CapitalsAssessment.Core.Dtos;

namespace CapitalsAssessment.Core.Interfaces
{
    public interface IEmployeeApplicationRepository
    {
        Task<EmployeeApplicationDto> ApplyAsync(string programId, EmployeeApplicationDto employeeApplicationDto);
        Task<EmployeeApplicationDto> UpdateApplication(EmployeeApplicationDto input);
    }
}
