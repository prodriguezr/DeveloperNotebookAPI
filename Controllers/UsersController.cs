using Microsoft.AspNetCore.Mvc;
using DeveloperNotebookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using DeveloperNotebookAPI.Helpers;

namespace DeveloperNotebookAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class UsersController : MyControllerBase 
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var vUser = Authenticate(model.Username, model.Password);

            if (vUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            UpdateUserToken(vUser);

            return Ok(vUser);
        }

        private static bool UpdateUserToken(User thisUser) 
        {
            bool vReturn;

            try 
            {
                // Save user token
                using (var ctx = new MyDbContext())
                {
                    var vMyUser = ctx.Users
                                    .Where(u => u.Id.Equals(thisUser.Id))
                                    .FirstOrDefault<User>();
                    
                    if (vMyUser == null)
                        vReturn = false;
                    else
                    {
                        vMyUser.Token = thisUser.Token;
                        ctx.SaveChanges();

                        vReturn = true;                  
                    }
                }
            }
            catch(Exception) 
            {
                vReturn = false;
            }

            return vReturn;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            using (var ctx = new MyDbContext())
            {
                if (ctx.Users.Any())
                    return Ok(ctx.Users);

                return NoContent();
            }
        }

        // GET: api/users/2
        [HttpGet("{id}")]
        private ActionResult<User> GetUserById(int id)
        {
            using (var ctx = new MyDbContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Id.Equals(id));

                if (user != null)
                    return Ok(user);

                return NotFound();
            }
        }

        private static User GetUserByName(string username)
        {
            using (var ctx = new MyDbContext())
            {
                return ctx.Users.FirstOrDefault(u => u.Username.Equals(username));
            }
        }

        private static User Authenticate(string username, string password)
        {
            User vUser = GetUserByName(username);

            if (vUser == null)
                return null;

            if (!vUser.Password.Equals(Helpers.Security.Encrypt(password, 30)))
                return null;

            vUser.Token = GenerateToken(vUser.Username);

            return vUser.WithoutPassword();
        }

        private static string GenerateToken(string username)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Globals.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}