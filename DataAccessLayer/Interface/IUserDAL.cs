using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelLayer.Entity;

namespace DataAccessLayer.Interface
{
    public interface IUserDAL
    {
        Task<User?> GetUserByEmail(string email);

        Task<Role?> GetRoleByName(string roleName);

        Task AddUser(User user);
    }
}