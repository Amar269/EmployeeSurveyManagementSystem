using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Survey
{
    public class SubmittedSurveyDto
    {
        public int SurveyId { get; set; }

        public string Title { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}