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
        public IEnumerable<CategoryName> names()
        {
            List<CategoryName> categories = new List<CategoryName>();
            foreach (var category in context.Categories)
            {
                categories.Add(new CategoryName(category.Name));
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
        public string post([FromBody] NewCategory category)
        {
            Category cat = new Category();
            cat.ID = category.ID;
            cat.Name = category.Name;
            cat.Norm_h = category.Norm_h;
            if(category.Parent != "No parent")
            cat.Parent = context.Categories.Where(c => c.Name == category.Parent).FirstOrDefault().ID;
            context.Categories.Add(cat);
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
