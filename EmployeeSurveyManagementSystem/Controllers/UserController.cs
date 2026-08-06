using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _userBLL.LoginUser(request);

                return Ok(new
                {
                    success = true,
                    message = "Login Successful",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("JWT Token Verified Successfully");
        }
    }


}