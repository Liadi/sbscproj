

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Models {
    public class UserExam
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string ExamId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public decimal ScorePercentage { get; set; }

        private DateTime dateTaken = new DateTime();

        public DateTime DateTaken {get {return dateTaken;} }
    }
}