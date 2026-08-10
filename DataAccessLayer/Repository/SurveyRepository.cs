using System;
using System.Collections.Generic;
using System.Linq;
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

       
        public async Task<SurveyDto> GetSurveyAsync(int surveyId)
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.SurveyId == surveyId);

            if (survey == null)
                return null;

            return new SurveyDto
            {
                SurveyId = survey.SurveyId,
                Title = survey.Title,
                Description = survey.Description,

                Questions = survey.Questions
                    .Select(q => new QuestionDto
                    {
                        QuestionId = q.QuestionId,
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType
                    })
                    .ToList()
            };
        }

       
        public async Task<bool> SubmitSurveyAsync(
            int userId,
            SubmitSurveyRequest request)
        {
            foreach (var answer in request.Answers)
            {
                var response = new Response
                {
                    UserId = userId,
                    SurveyId = request.SurveyId,
                    QuestionId = answer.QuestionId,
                    Answer = answer.Answer,
                    SubmittedAt = DateTime.Now
                };

                await _context.Responses.AddAsync(response);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        
        public async Task<bool> HasUserSubmittedSurveyAsync(
            int userId,
            int surveyId)
        {
            return await _context.Responses
                .AnyAsync(r =>
                    r.UserId == userId &&
                    r.SurveyId == surveyId);
        }

        
        public async Task<List<SurveyListDto>> GetAllSurveysAsync()
        {
            return await _context.Surveys
                .Select(s => new SurveyListDto
                {
                    SurveyId = s.SurveyId,
                    Title = s.Title
                })
                .ToListAsync();
        }

        public async Task<List<SubmittedSurveyDto>> GetSubmittedSurveysAsync(int userId)
        {
            return await _context.Responses
                .Where(r => r.UserId == userId)
                .GroupBy(r => new
                {
                    r.SurveyId,
                    r.Survey.Title
                })
                .Select(g => new SubmittedSurveyDto
                {
                    SurveyId = g.Key.SurveyId,
                    Title = g.Key.Title,
                    SubmittedAt = g.Max(r => r.SubmittedAt)
                })
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
                }
    }
}