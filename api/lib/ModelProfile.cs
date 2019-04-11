using AutoMapper;
using Interview.Models;
using Interview.ViewModels;

namespace Interview
{
    public class ModelProfile: Profile
    {
        public ModelProfile () {
            CreateMap<Exam, ExamViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<Question, QuestionViewModel>();
        }
    }
}