using System;
using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    }
}
