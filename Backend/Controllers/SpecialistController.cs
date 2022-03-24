using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {

        private readonly DatabaseContext context;

        public SpecialistController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Specialist> get()
        {
            return context.Specialists;
        }

        [HttpGet("names")]
        public IEnumerable<string> names()
        {
            List<string> names = new List<string>();
            foreach(var specialist in context.Specialists)
            {
                names.Add(specialist.Name);
            }
            return names;
        }

        [HttpGet("names/{id}")]
        public string name(int id)
        {
            return context.Specialists.Where(s => s.ID == id).FirstOrDefault()?.Name;
        }

        [HttpGet("all/{id}")]
        public Specialist? getById(int id) { 
            return context.Specialists.Where(s => s.ID == id).FirstOrDefault(); 
        }

        [HttpPost("add")]
        public int post([FromBody] Specialist specialist)
        {
            context.Specialists.Add(specialist);
            context.SaveChanges();
            return 0;
        }

        [HttpDelete("{id}")]
        public int delete(int id)
        {
            context.Specialists.Remove(context.Specialists.Where(s => s.ID == id).First());
            context.SaveChanges();
            return 0;
        }

    }

}
