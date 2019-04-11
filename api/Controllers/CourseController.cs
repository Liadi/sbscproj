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
    [Route("api/course")]
    [ApiController]
    public class CourseController : Controller
    {
        ProjDbContext projDbContext;
        IMapper mapper;

        IConfiguration config;

        public CourseController (ProjDbContext projDbContext, IMapper mapper, IConfiguration config) {
            this.projDbContext = projDbContext;
            this.mapper = mapper;
            this.config = config;
        }

        [HttpGet ("GetAll")]
        public IActionResult GetAll () {
            List<Course> allCourses = projDbContext.Course.ToList();
            return Ok(allCourses);
        }


        [HttpGet ("{id}")]
        public IActionResult Get (string id)
        {
            Course course = projDbContext.Course.Where(c => c.Id == id).FirstOrDefault();
            if (course is null) {
                return NotFound(new GenericPayload("Course not found"));
            }
            return Ok(course);
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Course course) {
            if (course is null) {
                return BadRequest();
            }
            if (String.IsNullOrWhiteSpace(course.Content) || String.IsNullOrWhiteSpace(course.Name)) {
                return BadRequest(new GenericPayload("Content and Name are required"));
            }

            Course newCourse = new Course {
                    Content = course.Content,
                    Id = Guid.NewGuid().ToString(),
                    Name = course.Name,
            };
            projDbContext.Course.Add(
                newCourse
            );

            projDbContext.SaveChanges();

            return Ok(new GenericPayload("Course posted"));
        }


        [HttpPut]
        public IActionResult Put ([FromBody] Course course) {
            var currentUser = HttpContext.User;
            if (currentUser.FindFirst(c => c.Type == "typ" && c.Value == UserType.IT.ToString()) is null) {
                return Unauthorized();
            }

            if (course is null) {
                return BadRequest();
            }

            Course dbCourse = projDbContext.Course.Find(course.Id);

            if (dbCourse is null) {
                return NotFound();
            }

            dbCourse.Content = String.IsNullOrWhiteSpace(course.Content) ? dbCourse.Content : course.Content;
            dbCourse.Id = String.IsNullOrWhiteSpace(course.Id) ? dbCourse.Id : course.Id;
            dbCourse.Name = String.IsNullOrWhiteSpace(course.Name) ? dbCourse.Name : course.Name;

            projDbContext.SaveChanges();

            return Ok(dbCourse);
        } 

    }
}
