using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }

        public string Body { get; set; }

        public string Answer { get; set; }

        public string ExamId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
    }
}