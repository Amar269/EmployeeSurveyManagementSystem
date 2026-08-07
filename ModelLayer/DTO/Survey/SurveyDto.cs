using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Survey
{
    public class SurveyDto
    {
        public int SurveyId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
