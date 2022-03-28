using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {

        private readonly DatabaseContext context;

        public SpecialityController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<NewSpeciality> get()
        {
            LinkedList<NewSpeciality> specialities = new LinkedList<NewSpeciality>();
            Dictionary<int, string> categories = new Dictionary<int, string>();
            foreach (var category in context.Categories)
            {
                categories[category.ID] = category.Name;
            }
            foreach (var speciality in context.Specialities)
            {
                NewSpeciality spec = new NewSpeciality();
                spec.Name = speciality.Name;
                spec.ID = speciality.ID;
                spec.CategoryName = categories[speciality.CategoryID];
               
                specialities.AddLast(spec);
            }
            return specialities;
        }

        [HttpGet("names")]
        public IEnumerable<string> names()
        {
            List<string> names = new List<string>();
            foreach (var specialist in context.Specialities)
            {
                names.Add(specialist.Name);
            }
            return names;
        }

        [HttpGet("names/{id}")]
        public string? name(int id)
        {
            return "{\"name\":\"" + context.Specialities.Where(s => s.ID == id).FirstOrDefault()?.Name + "\"}";
        }

        [HttpGet("all/{id}")]
        public NewSpeciality? getById(int id)
        {
            Speciality speciality = context.Specialities.Where(d => d.ID == id).FirstOrDefault();
            NewSpeciality spec = new NewSpeciality();
            spec.ID = speciality.ID;
            spec.Name = speciality.Name;
            spec.CategoryName = context.Categories.Where(c => c.ID == speciality.CategoryID).FirstOrDefault().Name;
            return spec;
        }

        [HttpPost("add")]
        public string post([FromBody] NewSpeciality speciality)
        {
            Speciality spec = new Speciality();
            spec.ID = speciality.ID;
            spec.Name = speciality.Name;
            Category category = context.Categories.Where(c => c.Name == speciality.CategoryName).FirstOrDefault();
            if (category == null)
            {
                return "{\"response\":0}";
            }
            else
            {
                spec.CategoryID = category.ID;
                context.Specialities.Add(spec);
                context.SaveChanges();
                return "{\"response\":1}";
            }
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Specialities.Remove(context.Specialities.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
