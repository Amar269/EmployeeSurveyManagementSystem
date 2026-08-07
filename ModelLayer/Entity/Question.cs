using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entity
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionType { get; set; }

        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}