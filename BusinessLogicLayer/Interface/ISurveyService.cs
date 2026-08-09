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
        Task<SurveyDto> GetSurveyAsync();

        Task<bool> SubmitSurveyAsync(int userId, SubmitSurveyRequest request);

        Task<bool> HasUserSubmittedSurveyAsync(int userId);

        Task<List<SurveyListDto>> GetAllSurveysAsync();
    }
}
