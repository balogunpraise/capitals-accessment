using CapitalsAssessment.Core.APIResponse;
using CapitalsAssessment.Core.Dtos;
using CapitalsAssessment.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapitalsAssessment.Api.Controllers
{
    public class ApplicationController(IEmployeeApplicationRepository employeeApplicationRepository) : BaseApiController
    {
        private readonly IEmployeeApplicationRepository _employeeApplicationRepository = employeeApplicationRepository;


        [HttpPost("SubmitApplication")]
        public async Task<ActionResult<BaseResponse<EmployeeApplicationDto>>> Apply(EmployeeApplicationDto input)
        {
            var result = await _employeeApplicationRepository.ApplyAsync(input.ProgramId, input);
            if (result is not null)
                return BaseResponse<EmployeeApplicationDto>.Success("Success", result, 200);
            return BadRequest();
        }

        [HttpPut("UpdateApplication")]
        public async Task<ActionResult<BaseResponse<EmployeeApplicationDto>>> UpdateApplication(EmployeeApplicationDto input)
        {
            var result = await _employeeApplicationRepository.UpdateApplication(input);
            if (result is not null)
                return BaseResponse<EmployeeApplicationDto>.Success("Success", result, 200);
            return BadRequest();
        }


        [HttpGet("GetApplication/{id}")]
        public ActionResult GetEmployeeApplicationById(string id)
        {
            return Ok();
        }
    }
}
