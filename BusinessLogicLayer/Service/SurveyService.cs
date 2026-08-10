using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Interface;
using ModelLayer.DTO.Survey;

namespace BusinessLogicLayer.Service
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<SurveyDto> GetSurveyAsync(int surveyId)
        {
            return await _surveyRepository.GetSurveyAsync(surveyId);
        }
        public async Task<bool> SubmitSurveyAsync(
            int userId,
            SubmitSurveyRequest request)
        {
            var alreadySubmitted =
                await _surveyRepository.HasUserSubmittedSurveyAsync(
        userId,
        request.SurveyId
    );

            if (alreadySubmitted)
            {
                return false;
            }

            return await _surveyRepository.SubmitSurveyAsync(
                userId,
                request);
        }

        public async Task<bool> HasUserSubmittedSurveyAsync(int userId, int surveyId)
        {
            return await _surveyRepository.HasUserSubmittedSurveyAsync(userId, surveyId);
        }

        public async Task<List<SurveyListDto>> GetAllSurveysAsync()
        {
            return await _surveyRepository.GetAllSurveysAsync();
        }

        public async Task<List<SubmittedSurveyDto>> GetSubmittedSurveysAsync(int userId)
        {
            return await _surveyRepository.GetSubmittedSurveysAsync(userId);
        }
    }
}