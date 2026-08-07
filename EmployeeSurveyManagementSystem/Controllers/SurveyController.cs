using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Survey;
using System.Security.Claims;

namespace EmployeeSurveyManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSurvey()
        {
            var survey = await _surveyService.GetSurveyAsync();

            if (survey == null)
                return NotFound("Survey not found.");

            return Ok(survey);
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckSurveyStatus()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            bool submitted = await _surveyService.HasUserSubmittedSurveyAsync(userId);

            return Ok(new
            {
                Submitted = submitted
            });
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitSurvey([FromBody] SubmitSurveyRequest request)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            bool result = await _surveyService.SubmitSurveyAsync(userId, request);

            if (!result)
            {
                return BadRequest(new
                {
                    Message = "Survey already submitted."
                });
            }

            return Ok(new
            {
                Message = "Survey submitted successfully."
            });
        }
    }
}