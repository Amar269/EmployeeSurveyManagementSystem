using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Admin;
using System;

namespace EmployeeSurveyManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("responses")]
        public async Task<IActionResult> GetAllResponses()
        {
            var responses = await _adminService.GetAllResponsesAsync();

            return Ok(responses);

        }

        [HttpGet("surveys")]
        public async Task<IActionResult> GetAllSurveys()
        {
            var surveys = await _adminService.GetAllSurveysAsync();

            return Ok(surveys);
        }

        [HttpPost("surveys")]
        public async Task<IActionResult> AddSurvey([FromBody] AddSurveyRequest request)
        {
            var surveyId = await _adminService.AddSurveyAsync(request);

            return Ok(new
            {
                Message = "Survey added successfully.",
                SurveyId = surveyId
            });
        }
    }
}
