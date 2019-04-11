using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interview.Models {
    public class Course
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        List <UserCourse> userCourse = new List<UserCourse>();
        public List <UserCourse> UserCourse {
            get { return userCourse; }
        }

        List <Exam> exam = new List<Exam>();
        List <Exam> Exam {
            get { return exam; }
        }
    }
}