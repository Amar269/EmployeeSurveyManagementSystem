using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entity
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public string Answer { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}