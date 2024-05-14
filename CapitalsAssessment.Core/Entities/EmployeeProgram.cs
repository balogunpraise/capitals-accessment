using Newtonsoft.Json;

namespace CapitalsAssessment.Core.Entities
{
    public class EmployeeProgram : BaseEntity
    {
        [JsonProperty("programTitle")]
        public string ProgramTitle { get; set; }

        [JsonProperty("programDescription")]
        public string ProgramDescription { get; set; }

        [JsonProperty("questions")]
        public ICollection<CustomQuestion> Questions { get; set; }

    }
}
