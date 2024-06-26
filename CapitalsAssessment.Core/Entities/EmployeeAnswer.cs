﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CapitalsAssessment.Core.Entities
{
    public class EmployeeAnswer : BaseEntity
    {
        [JsonProperty("questionId")]
        [Required]
        public string QuestionId { get; set; }

        [JsonProperty("questionDescription")]
        public string QuestionDescription { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
    }
}
