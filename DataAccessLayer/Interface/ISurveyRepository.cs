using ModelLayer.DTO.Survey;

namespace DataAccessLayer.Interface
{
    public interface ISurveyRepository
    {
        Task<SurveyDto> GetSurveyAsync(int surveyId);

        Task<bool> SubmitSurveyAsync(int userId, SubmitSurveyRequest request);

        Task<bool> HasUserSubmittedSurveyAsync(int userId, int surveyId);

        Task<List<SurveyListDto>> GetAllSurveysAsync();

        Task<List<SubmittedSurveyDto>> GetSubmittedSurveysAsync(int userId);

        Task<List<SubmittedResponseDto>> GetSubmittedResponsesAsync(int userId,int surveyId);
    }
}