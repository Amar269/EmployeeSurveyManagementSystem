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

        public async Task<SurveyDto> GetSurveyAsync()
        {
            return await _surveyRepository.GetSurveyAsync();
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