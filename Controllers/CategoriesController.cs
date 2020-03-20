using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DeveloperNotebookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DeveloperNotebookAPI.Models.Entity;

namespace DeveloperNotebookAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController
    {
        public CategoriesController()
        {}

        // GET: api/categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await GetAllCategoryAsync();

            return categories;
        }

        private async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            using (var ctx = new MyDbContext())
            {
                return await ctx.Categories.ToListAsync();
            }
        }

        // GET: api/categories/3
        [HttpGet("{id}")]
        public async Task<Category> GetCategoryById(int id)
        {
            using (var ctx = new MyDbContext())
            {
                return await GetCategoryByIdAsync(id);
            }
        }

        private async Task<Category> GetCategoryByIdAsync(int id)
        {
            using (var ctx = new MyDbContext())
            {
                return await ctx.Categories.FirstOrDefaultAsync(c => c.Id == id && c.ActiveRecord == true);
            }
        }

/*
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
*/
    }
}