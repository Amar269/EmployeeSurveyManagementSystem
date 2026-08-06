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

namespace BusinessLogicLayer.Service
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;

        public UserBLL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
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
