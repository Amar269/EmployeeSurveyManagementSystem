using ModelLayer.DTO.Admin;
using ModelLayer.DTO.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IAdminRepository
    {
        Task<List<AdminResponseDto>> GetAllResponsesAsync();

        Task<List<SurveyListDto>> GetAllSurveysAsync();

        Task<int> AddSurveyAsync(AddSurveyRequest request);

        Task<bool> DeleteSurveyAsync(int surveyId);

    }
}
