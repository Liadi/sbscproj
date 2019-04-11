using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Models {
    public class Exam
    {
        [Key]
        public string Id { get; set; }

        public string Description { get; set; }

        int passScorePercentage = 0;

        public int PassScorePercentage { get { return passScorePercentage; } set{ passScorePercentage = value; } }


        List <Question> questions = new List <Question>();

        public List <Question> Questions {
            get { return questions; }
        }

        List <UserExam> userExam = new List <UserExam>();

        public List <UserExam> UserExam {
            get { return userExam; }
        }

        public string CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}