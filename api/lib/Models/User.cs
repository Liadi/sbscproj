using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Interview.Models
{
    [Serializable]
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Salt { get;set; }

        public string Password { get;set; }

        string profile = "";

        public string Profile { 
            get {return profile;}
            set {profile = value;}
        }

        // [NotMapped]
        List <UserCourse> userCourse = new List<UserCourse> ();
        public List <UserCourse> UserCourse {
            get { return userCourse; }
        }

        List <UserExam> userExam = new List<UserExam>();
        public List <UserExam> UserExam {
            get { return userExam; }
        }

        public UserType UserType { get; set; }
    }
}
