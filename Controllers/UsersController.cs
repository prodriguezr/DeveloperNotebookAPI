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
        public UsersController(MyDbContext context) : base(context)
        {}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var vUser = Authenticate(model.Username, model.Password);

            if (vUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            UpdateUserToken(vUser.Id, vUser.Token);

            return Ok(vUser);
        }

        private static bool UpdateUserToken(int userId, string token) 
        {
            bool vReturn;

            try 
            {
                // Save user token
                using (var ctx = new MyDbContext())
                {
                    var vMyUser = ctx.Users
                                    .Where(u => u.Id == userId)
                                    .FirstOrDefault<User>();
                    
                    if (vMyUser == null)
                        vReturn = false;
                    else
                    {
                        vMyUser.Token = token;
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
            return myDbContext.Users;
        }

        private static User ExistUser(string username, out bool exists)
        {
            using (var ctx = new MyDbContext())
            {
                var vUser = ctx.Users
                                .Where(s => s.Username == username)
                                .FirstOrDefault<User>();

                exists = vUser == null ? false : true;

                return vUser;
            }
        }
        private User Authenticate(string username, string password)
        {
            User vUser = null;

            using (var ctx = new MyDbContext())
            {
                vUser = ctx.Users
                        .Where(s => s.Username == username)
                        .FirstOrDefault<User>();
            }
            
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