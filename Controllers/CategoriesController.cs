using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DeveloperNotebookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DeveloperNotebookAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController : MyControllerBase 
    {
        public CategoriesController(MyDbContext context) : base(context)
        {}

        // GET: api/categories
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return myDbContext.Categories;
        }

        // GET: api/categories/3
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = myDbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            return category;
        }

        // POST: api/categories
        [HttpPost]
        public ActionResult<Category> PostCategory(Category category)
        {
            myDbContext.Categories.Add(category);
            myDbContext.SaveChanges();

            return CreatedAtAction("GetCategoryById", new Category {Id = category.Id}, category);
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public ActionResult PutCategory(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            myDbContext.Entry(category).State = EntityState.Modified;
            myDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/categories/4
        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            var category = myDbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            myDbContext.Categories.Remove(category);
            myDbContext.SaveChanges();

            return category;
        }
    }
}