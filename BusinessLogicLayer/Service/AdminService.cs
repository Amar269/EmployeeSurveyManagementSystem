using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Interface;
using ModelLayer.DTO.Admin;
using ModelLayer.DTO.Survey;

namespace BusinessLogicLayer.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<int> AddSurveyAsync(AddSurveyRequest request)
        {
            return await _adminRepository.AddSurveyAsync(request);
        }

        public async Task<List<AdminResponseDto>> GetAllResponsesAsync()
        {
            return await _adminRepository.GetAllResponsesAsync();
        }

        public  async Task<List<SurveyListDto>> GetAllSurveysAsync()
        {
            return await _adminRepository.GetAllSurveysAsync();
        }
    }
}
