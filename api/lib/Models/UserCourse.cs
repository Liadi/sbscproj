using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Models {
    public class UserCourse
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}