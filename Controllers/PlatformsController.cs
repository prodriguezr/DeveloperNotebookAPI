using System.Collections.Generic;
using System.Linq;
using DeveloperNotebookAPI.Models;
using DeveloperNotebookAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperNotebookAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class PlatformsController: ControllerBase
    {
        // GET: api/platforms
        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAll()
        {
            using (var ctx = new MyDbContext())
            {
                if (ctx.Platforms.Any())
                    return Ok(ctx.Platforms);

                return NoContent();
            }
        }
       
        // GET: api/platforms/1
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Platform>> GetPlatformById(int id)
        {
            using (var ctx = new MyDbContext())
            {
                var platform = ctx.Platforms.FirstOrDefault(p => p.Id.Equals(id));

                if (platform != null)
                    return Ok(platform);

                return NotFound();
            }
        }
    }
}