using CapitalsAssessment.Core.APIResponse;
using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Entities;
using CapitalsAssessment.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapitalsAssessment.Api.Controllers
{
    public class ProgramController(IRecruitementProgramRepository employeeProgramRepository) : BaseApiController
    {
        private readonly IRecruitementProgramRepository _employeeProgramRepository = employeeProgramRepository;

        [HttpPost("CreateOrUpdateProgram")]
        public async Task<ActionResult<BaseResponse<RecruitementProgram>>> CreateProgramOrUpdate(CreateProgramDto input)
        {
            var result = await _employeeProgramRepository.CreateNewOrUpdateProgram(input);
            if (result is not null)
                return BaseResponse<RecruitementProgram>.Success("Succeeded", result, 200);
            return BadRequest();
        }

        /*     [HttpPut]
             public async Task<ActionResult> UpdateProgram(CreateProgramDto input)
             {
                 return Ok();
             }*/

        [HttpGet("GetApplicationQuestions/{programId}")]
        public async Task<ActionResult<BaseResponse<CreateProgramDto>>> GetProgramAndQuestions(string programId)
        {
            var result = await _employeeProgramRepository.GetProgram(programId);
            if (result is not null)
                return BaseResponse<CreateProgramDto>.Success("Succeeded", result, 200);
            return BadRequest(result);
        }
    }
}
