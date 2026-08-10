using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelLayer.DTO.Admin
{
    public class AddSurveyRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<AddQuestionRequest> Questions { get; set; } = new();
    }

    public class AddQuestionRequest
    {
        public string QuestionText { get; set; } = string.Empty;

        public string QuestionType { get; set; } = string.Empty;
    }
}