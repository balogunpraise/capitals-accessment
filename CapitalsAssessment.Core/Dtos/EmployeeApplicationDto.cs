using CapitalsAssessment.Core.Entities;

namespace CapitalsAssessment.Core.Dtos
{
    public class EmployeeApplicationDto
    {
        public string Id { get; set; }

        //[Required]
        public string ProgramId { get; set; }
        public ICollection<EmployeeAnswer> EmployeeAnswers { get; set; }
    }
}
