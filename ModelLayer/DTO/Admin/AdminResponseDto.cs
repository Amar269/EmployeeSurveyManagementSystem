using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Admin
{
    public class AdminResponseDto
    {
        public int ResponseId { get; set; }

        public int UserId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Email { get; set; }

        public int SurveyId { get; set; }

        public string SurveyTitle { get; set; } = string.Empty;

        public int QuestionId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;

        public  DateTime SubmittedAt  { get; set; }


    }
}
