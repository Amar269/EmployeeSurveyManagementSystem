using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.User;

namespace EmployeeSurveyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBLL _userBLL;

        public UserController(IUserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userBLL.RegisterUser(request);

            return Ok(new
            {
                success = true,
                message = "User Registered Successfully",
                data = result
            });
        }
    }


}