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
        public IEnumerable<Speciality> get()
        {
            return context.Specialities;
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
        public Speciality? getById(int id)
        {
            return context.Specialities.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public string post([FromBody] Speciality speciality)
        {
            context.Specialities.Add(speciality);
            context.SaveChanges();
            return "{\"response\":0}";
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
