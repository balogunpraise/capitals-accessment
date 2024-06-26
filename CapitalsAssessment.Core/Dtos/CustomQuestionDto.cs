﻿using CapitalsAssessment.Core.Enums;

namespace CapitalsAssessment.Core.Dtos
{
    public class CustomQuestionDto
    {
        public string Id { get; set; }

        public string ProgramId { get; set; }

        public string QuesttionDescription { get; set; }

        public FieldEnums FieldType { get; set; }

        public string Answer { get; set; }

        public bool IsRequired { get; set; }

        public bool IsHidden { get; set; }

        public bool IsInternal { get; set; }
        public bool IsDropDown { get; set; }
        public List<OptionsDto> DropdownOptions { get; set; }
    }
}
