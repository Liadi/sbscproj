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

namespace Interview.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        ProjDbContext projDbContext;
        IMapper mapper;

        IConfiguration config;

        public UserController (ProjDbContext projDbContext, IMapper mapper, IConfiguration config) {
            this.projDbContext = projDbContext;
            this.mapper = mapper;
            this.config = config;
        }

        [HttpGet ("GetAll")]
        public IActionResult GetAll () {
            List<User> allUsers = projDbContext.User.ToList();
            List<UserViewModel> allViewModelUsers = Mapper.Map<List<User>, List<UserViewModel>>(allUsers);
            return Ok(allViewModelUsers);
        }


        [HttpPost]
        public IActionResult Login ([FromBody] User userEntry)
        {
            if (String.IsNullOrWhiteSpace(userEntry.Username) || String.IsNullOrWhiteSpace(userEntry.Password)) {
                return BadRequest(new GenericPayload ("Username and Password required"));
            }
            User user = projDbContext.User.Find(userEntry.Username);
            if (user  is null) {
                return NotFound(
                    new GenericPayload ("Incorrect Username or Password")
                );
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userEntry.Password,
                salt: Convert.FromBase64String(user.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed != user.Password) {
                return NotFound(
                    new GenericPayload ("Incorrect Username or Password")
                );
            }

            UserViewModel userViewModel = mapper.Map<UserViewModel>(user);

            Util.GenerateJSONWebToken(userViewModel, config.GetSection ("Jwt") ["Key"], config.GetSection ("Jwt") ["Issuer"]);

            return Ok(userViewModel);
        }

        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] User userEntry)
        {
            if (String.IsNullOrWhiteSpace(userEntry.Username) || String.IsNullOrWhiteSpace(userEntry.Password)) {
                return BadRequest(
                    new GenericPayload ("Username and Password required")
                );
            }
            User user = projDbContext.User.Find(userEntry.Username);
            if (user  != null) {
                return BadRequest(
                    new GenericPayload("User already exists")
                );
            }
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userEntry.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            projDbContext.User.Add(
                new User() {
                    Username = userEntry.Username,
                    Password = hashed,
                    Profile = userEntry.Profile,
                    Salt = Convert.ToBase64String(salt)
                }
            );

            projDbContext.SaveChanges();

            return Ok(new GenericPayload("account created"));
        }
    }
}
