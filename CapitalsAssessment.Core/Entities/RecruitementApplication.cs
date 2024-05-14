using Newtonsoft.Json;

namespace CapitalsAssessment.Core.Entities
{
    public class RecruitementApplication : BaseEntity
    {
        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("customQuestions")]
        public ICollection<EmployeeAnswer> EmployeeAnswers { get; set; }
    }
}
