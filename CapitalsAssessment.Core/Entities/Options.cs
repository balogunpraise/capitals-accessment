using Newtonsoft.Json;

namespace CapitalsAssessment.Core.Entities
{
    public class Options : BaseEntity
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }

        [JsonProperty("optionName")]
        public string OptionName { get; set; }
    }
}
