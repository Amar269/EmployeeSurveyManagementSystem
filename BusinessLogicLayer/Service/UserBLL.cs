using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DataAccessLayer.Interface;
using BusinessLogicLayer.Interface;
using ModelLayer.DTO.User;
using ModelLayer.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace BusinessLogicLayer.Service
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;
        private readonly IConfiguration _configuration;

        public UserBLL(IUserDAL userDAL , IConfiguration configuration  )
        {
            _userDAL = userDAL;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await _userDAL.GetUserByEmail(request.Email);

            if (user == null)
            {
                throw new Exception("Invalid Email");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid Password");
            }

            string key = _configuration["Jwt:Key"]!;
            string issuer = _configuration["Jwt:Issuer"]!;
            string audience = _configuration["Jwt:Audience"]!;
            int duration = Convert.ToInt32(_configuration["Jwt:DurationInMinutes"]);

            var claims = new List<Claim>
                    {
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.RoleName)
                    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: credentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Token = jwtToken
            };
        }

        public async Task<UserResponse> RegisterUser(RegisterRequest request)
        {
            var existingUser = await _userDAL.GetUserByEmail(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var role = await _userDAL.GetRoleByName("Employee");

            if (role == null)
            {
                throw new Exception("Employee role not found.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashedPassword,
                RoleId = role.RoleId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            await _userDAL.AddUser(user);

            return new UserResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = role.RoleName
            };
        }
    }


}
