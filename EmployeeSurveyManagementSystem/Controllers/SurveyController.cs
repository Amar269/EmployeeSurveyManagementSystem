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

        // GET SELECTED SURVEY
        [HttpGet("{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId)
        {
            var survey = await _surveyService.GetSurveyAsync(surveyId);

            if (survey == null)
                return NotFound("Survey not found.");

            return Ok(survey);
        }

        // GET ALL SURVEYS
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSurveys()
        {
            var surveys = await _surveyService.GetAllSurveysAsync();

            return Ok(surveys);
        }

        // CHECK WHETHER CURRENT USER SUBMITTED THIS SURVEY
        [HttpGet("check/{surveyId}")]
        public async Task<IActionResult> CheckSurveyStatus(int surveyId)
        {
            int userId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            bool submitted =
                await _surveyService.HasUserSubmittedSurveyAsync(
                    userId,
                    surveyId
                );

            return Ok(new
            {
                Submitted = submitted
            });
        }

        // SUBMIT SURVEY
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitSurvey(
            [FromBody] SubmitSurveyRequest request)
        {
            int userId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            bool result =
                await _surveyService.SubmitSurveyAsync(
                    userId,
                    request
                );

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

        // GET SUBMITTED SURVEYS FOR CURRENT USER
        [HttpGet("submitted")]
        public async Task<IActionResult> GetSubmittedSurveys()
        {
            int userId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var surveys = await _surveyService.GetSubmittedSurveysAsync(userId);

            return Ok(surveys);
        }
    }
}