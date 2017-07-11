using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIWorkshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APIWorkshop.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost("api/auth/token")]
        public IActionResult CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                const string userName = "Asia";
                const string password = "Haslo123";

                var user = new User
                {
                    Name = userName,
                    Password = password
                };
                
                if (model.UserName != userName)
                {
                    return NotFound();
                }

                if (password == model.Password)
                {
                    var claims = new []
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKeyANdVerVeryLong"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                                issuer: "http://localhost:8888",
                                audience: "http://localhost:8888",
                                claims: claims,
                                expires: DateTime.UtcNow.AddMinutes(15),
                                signingCredentials: creds
                                );
                    return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo

                        });
                }
            }
            catch
            {
                return StatusCode(500);
            }

            return BadRequest();
        }
    }
}
