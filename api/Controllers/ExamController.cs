using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interview.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using AutoMapper;
using Interview.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Interview.Controllers
{
    [Authorize]
    [Route("api/exam")]
    [ApiController]
    public class ExamController : Controller
    {
        ProjDbContext projDbContext;
        IMapper mapper;

        IConfiguration config;

        public ExamController (ProjDbContext projDbContext, IMapper mapper, IConfiguration config) {
            this.projDbContext = projDbContext;
            this.mapper = mapper;
            this.config = config;
        }

        [HttpGet ("GetAll")]
        public IActionResult GetAll () {
            List<Exam> allExam = projDbContext.Exam.ToList();
            return Ok(allExam);
        }

        [HttpGet ("GetAllCourse/{courseId}")]
        public IActionResult GetAllCourse (string courseId) {
            List<Exam> allExam = projDbContext.Exam.Where(e => e.CourseId == courseId).ToList();
            return Ok(allExam);
        }

        [HttpGet ("GetAllUser/{userId}")]
        public IActionResult GetAllUser (string userId) {
            List<Exam> allExam = projDbContext.Exam.Where(e => e.UserExam.Any(ec => ec.UserId == userId)).ToList();
            return Ok(allExam);
        }

        [HttpGet ("{id}")]
        public IActionResult Get (string id)
        {
            Exam exam = projDbContext.Exam.Where( e => e.Id == id).FirstOrDefault();

            if (exam is null) {
                return NotFound(
                    new {
                        msg = "Exam not found"
                    }
                );
            }

            List<Question> questions = projDbContext.Question.Where(q => q.ExamId == exam.Id).ToList();

            ExamViewModel examViewModel = mapper.Map<ExamViewModel>(exam);
            List<QuestionViewModel> allQuestionViewModel = Mapper.Map<List<Question>, List<QuestionViewModel>>(questions);

            examViewModel.Questions = allQuestionViewModel;
            return Ok(examViewModel);
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Exam exam) {
            var currentUser = HttpContext.User;
            if (currentUser.FindFirst(c => c.Type == "typ" && c.Value == UserType.IT.ToString()) is null) {
                return Unauthorized();
            }

            if (exam.Questions.Count() == 0 || String.IsNullOrWhiteSpace(exam.Description)) {
                return BadRequest(new GenericPayload("Format error: set Description and Questions"));
            }

            string examId = Guid.NewGuid().ToString();
            List<Question> questionList = new List<Question>();

            foreach(Question question in exam.Questions){
                if (String.IsNullOrWhiteSpace(question.Answer) || String.IsNullOrWhiteSpace(question.Body)) {
                    return BadRequest(new GenericPayload("Format error: set Questios (with Body and Answer) and PassScorePercentage"));
                }
                questionList.Add(new Question {
                    Id = Guid.NewGuid().ToString(),
                    ExamId = examId,
                    Answer = question.Answer,
                    Body = question.Body
                });
            }

            Exam newExam = new Exam {
                    Description = exam.Description,
                    Id = examId,
                    PassScorePercentage = exam.PassScorePercentage,
                };
            newExam.Questions.AddRange(questionList);
            projDbContext.Exam.Add(
                newExam
            );

            projDbContext.SaveChanges();

            return Ok(new GenericPayload("Exam posted"));
        } 

        [HttpPut ("UpdateQuestion/{examId}")]
        public IActionResult UpdateQuestion ([FromBody] Question question)
        {
            var currentUser = HttpContext.User;
            if (currentUser.FindFirst(c => c.Type == "typ" && c.Value == UserType.IT.ToString()) is null) {
                return Unauthorized();
            }
            Question presQuestion = projDbContext.Question.Find(question.Id);
            if (presQuestion is null) {
                return NotFound(new GenericPayload("Question not found"));
            }
            presQuestion.Answer = String.IsNullOrWhiteSpace(question.Answer) ? presQuestion.Answer : question.Answer;
            presQuestion.Body = String.IsNullOrWhiteSpace(question.Body) ? presQuestion.Body : question.Body;
            projDbContext.SaveChanges();
            return Ok(presQuestion);
        }

        [HttpPost("UserSubmitExam")]
        public IActionResult UserSubmitExam([FromBody] Exam takenExam)
        {
            var currentUser = HttpContext.User;
            var claim = currentUser.FindFirst(c => c.Type == "sub");
            if (claim is null || String.IsNullOrWhiteSpace(claim.Value)) {
                return Unauthorized();
            }
            string userId = claim.Value;

            if (takenExam.Questions.Count == 0) {
                return Ok(new {
                    Percentage = 0
                });
            }
            
            Exam exam = projDbContext.Exam.Where(e => e.Id == takenExam.Id).Include(e => e.Questions).FirstOrDefault();
            if (exam.Questions.Count == 0) {
                return BadRequest(new GenericPayload("No question in exam"));
            }
            
            int correctAnswer = 0;
            foreach (Question tempQuestion in takenExam.Questions) {
                Question tempOriginalQuestion = exam.Questions.Find(q => q.Id == tempQuestion.Id);
                if (tempOriginalQuestion is null) {
                    continue;
                }
                if (tempOriginalQuestion.Answer == tempQuestion.Answer) {
                    correctAnswer++;
                }
            }

            decimal scorePercentage = Math.Round((100 * correctAnswer) / (decimal) exam.Questions.Count, 2);

            projDbContext.UserExam.Add(new UserExam {
                UserId = userId,
                ExamId = exam.Id,
                ScorePercentage = scorePercentage
            });

            projDbContext.SaveChanges();

            string passStatus = scorePercentage >= exam.PassScorePercentage ? "Pass" : "Fail"; 

            return Ok(new GenericPayload("You got a score of " + scorePercentage + "%. " + passStatus));
        }
    }
}
