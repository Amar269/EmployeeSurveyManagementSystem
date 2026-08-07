using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Survey
{
    public class SubmitSurveyRequest
    {
        public int SurveyId { get; set; }

        public List<SurveyAnswerDto> Answers { get; set; }
    }

    public class SurveyAnswerDto
    {
        public int QuestionId { get; set; }

        public string Answer { get; set; }
    }
}