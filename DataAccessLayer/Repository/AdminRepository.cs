using DataAccessLayer.Context;
using DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Admin;
using ModelLayer.DTO.Survey;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddSurveyAsync(AddSurveyRequest request)
        {
            var survey = new Survey
            {
                Title = request.Title,
                Description = request.Description
            };

            await _context.Surveys.AddAsync(survey);

            await _context.SaveChangesAsync();

            foreach (var question in request.Questions)
            {
                var newQuestion = new Question
                {
                    SurveyId = survey.SurveyId,
                    QuestionText = question.QuestionText,
                    QuestionType = question.QuestionType
                };

                await _context.Questions.AddAsync(newQuestion);
            }

            await _context.SaveChangesAsync();

            return survey.SurveyId;
        }

        public async Task<bool> DeleteSurveyAsync(int surveyId)
        {
            var survey = await _context.Surveys
        .Include(s => s.Questions)
        .FirstOrDefaultAsync(s => s.SurveyId == surveyId);

            if (survey == null)
            {
                return false;
            }

            // Delete responses related to this survey
            var responses = await _context.Responses
                .Where(r => r.SurveyId == surveyId)
                .ToListAsync();

            _context.Responses.RemoveRange(responses);

            // Delete questions related to this survey
            _context.Questions.RemoveRange(survey.Questions);

            // Delete the survey
            _context.Surveys.Remove(survey);

            await _context.SaveChangesAsync();

            return true;

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

        public async Task<List<SurveyListDto>> GetAllSurveysAsync()
        {
            return await _context.Surveys
                .Select(s => new SurveyListDto()
                {
                    SurveyId = s.SurveyId,
                    Title = s.Title,
                })
                .ToListAsync();


        }
    }
}
