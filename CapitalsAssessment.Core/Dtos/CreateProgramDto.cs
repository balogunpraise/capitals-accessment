namespace CapitalsAssessment.Core.Dtos
{
    public class CreateProgramDto
    {
        public string Id { get; set; }

        //[Required]
        public string ProgramTitle { get; set; }

        //[Required]
        public string ProgramDescription { get; set; }

        public List<CustomQuestionDto> Questions { get; set; }
    }
}
