using System.Collections.Generic;
using Interview.Models;

namespace Interview.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string Profile { get; set; }

        public string Token { get; set; }

        // [NotMapped]
        public List <UserCourse> UserCourse {get; set;}

        public List <UserExam> UserExam {get; set;}

        public UserType UserType { get; set; }
    }
}