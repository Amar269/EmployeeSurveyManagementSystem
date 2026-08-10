using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO.Survey;

namespace BusinessLogicLayer.Interface
{
    public interface ISurveyService
    {
        Task<SurveyDto> GetSurveyAsync(int surveyId);

        Task<bool> SubmitSurveyAsync(int userId, SubmitSurveyRequest request);

        Task<bool> HasUserSubmittedSurveyAsync(int userId, int surveyId);

        Task<List<SurveyListDto>> GetAllSurveysAsync();

        Task<List<SubmittedSurveyDto>> GetSubmittedSurveysAsync(int userId);
    }
}
