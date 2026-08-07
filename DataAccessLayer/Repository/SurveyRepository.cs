using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer.Context;
using DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Survey;
using ModelLayer.Entity;

namespace DataAccessLayer.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ApplicationDbContext _context;

        public SurveyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SurveyDto> GetSurveyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubmitSurveyAsync(int userId, SubmitSurveyRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasUserSubmittedSurveyAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}