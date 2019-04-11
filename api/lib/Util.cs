using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interview.Models;
using Interview.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace Interview {
    public class Util {
        public static void GenerateJSONWebToken (UserViewModel userInfo, string jwtKey, string jwtIssuer) {
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (jwtKey));
            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256Signature);

            Claim[] claims = new [] {
                new Claim (JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim (JwtRegisteredClaimNames.Typ, userInfo.UserType.ToString())
            };

            var token = new JwtSecurityToken (jwtIssuer,
                jwtIssuer,
                claims,
                expires : DateTime.Now.AddHours (4),
                signingCredentials : credentials);

            userInfo.Token = new JwtSecurityTokenHandler ().WriteToken (token);
        }

        internal static void GenerateJSONWebToken (UserViewModel userViewModel, object p1, object p2) {
            throw new NotImplementedException ();
        }
    }
}