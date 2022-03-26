using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly DatabaseContext context;

        public CategoryController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Category> get()
        {
            return context.Categories;
        }

        [HttpGet("names")]
        public IEnumerable<string> names()
        {
            List<string> categories = new List<string>();
            foreach (var category in context.Categories)
            {
                categories.Add(category.Name);
            }
            return categories;
        }

        [HttpGet("names/{id}")]
        public string? name(int id)
        {
            return "{\"name\":\"" + context.Categories.Where(s => s.ID == id).FirstOrDefault()?.Name + "\"}";
        }

        [HttpGet("all/{id}")]
        public Category? getById(int id)
        {
            return context.Categories.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public string post([FromBody] Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return "{\"response\":0}";
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Categories.Remove(context.Categories.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
