using ModelLayer.DTO.Survey;

namespace DataAccessLayer.Interface
{
    public interface ISurveyRepository
    {
        Task<SurveyDto> GetSurveyAsync();

        Task<bool> SubmitSurveyAsync(int userId, SubmitSurveyRequest request);

        Task<bool> HasUserSubmittedSurveyAsync(int userId);

        Task<List<SurveyListDto>> GetAllSurveysAsync();
    }
}