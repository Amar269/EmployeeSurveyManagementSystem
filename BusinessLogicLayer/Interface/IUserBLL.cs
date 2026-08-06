using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO.User;

namespace BusinessLogicLayer.Interface
{
    public interface IUserBLL
    {
        Task<UserResponse> RegisterUser(RegisterRequest request);
        Task<LoginResponse> LoginUser(LoginRequest request);
    }
}