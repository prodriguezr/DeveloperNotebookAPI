using System.Collections.Generic;
using DeveloperNotebookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperNotebookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PlatformsController: MyControllerBase
    {
       public PlatformsController(MyDbContext context) : base(context)
       {}

       // GET: api/platforms
       [HttpGet]
       public ActionResult<IEnumerable<Platform>> GetAll()
       {
           return myDbContext.Platforms;
       }
       
    }
}