using DataAccessLayer.Context;
using DataAccessLayer.Interface;
using ModelLayer.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<AdminResponseDto>> GetAllResponsesAsync()
        {
            return await _context.Responses
            .Include(r => r.User)
            .Include(r => r.Survey)
            .Include(r => r.Question)
            .Select(r => new AdminResponseDto
            {
                ResponseId = r.ResponseId,
                UserId = r.UserId,
                EmployeeName = r.User.FirstName + " " + r.User.LastName,
                Email = r.User.Email,
                SurveyId = r.SurveyId,
                SurveyTitle = r.Survey.Title,
                QuestionId = r.QuestionId,
                QuestionText = r.Question.QuestionText,
                Answer = r.Answer,
                SubmittedAt = r.SubmittedAt

            })
            .ToListAsync();
        }
    }
}
