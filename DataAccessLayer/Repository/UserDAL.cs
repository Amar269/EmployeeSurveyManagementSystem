using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

namespace DataAccessLayer.Repository
{
    public class UserDAL : IUserDAL
    {
        private readonly ApplicationDbContext _context;

        public UserDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task<Role?> GetRoleByName(string roleName)
        {
            return await _context.Roles
        .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
        .Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.Email == email);
        }
        }
    }
}


