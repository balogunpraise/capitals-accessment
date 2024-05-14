﻿using Newtonsoft.Json;

namespace CapitalsAssessment.Core.Entities
{
    public class EmployeeApplication : BaseEntity
    {
        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("customQuestions")]
        public ICollection<EmployeeAnswer> EmployeeAnswers { get; set; }
    }
}
