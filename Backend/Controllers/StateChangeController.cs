using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateChangeController : ControllerBase
    {

        private readonly DatabaseContext context;

        public StateChangeController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Statechange> get()
        {
            return context.Statechanges;
        }

        [HttpGet("all/{id}")]
        public Statechange? getById(int id)
        {
            return context.Statechanges.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public int post([FromBody] Statechange statechange)
        {
            context.Statechanges.Add(statechange);
            context.SaveChanges();
            return 0;
        }

        [HttpDelete("{id}")]
        public int delete(int id)
        {
            context.Statechanges.Remove(context.Statechanges.Where(s => s.ID == id).First());
            context.SaveChanges();
            return 0;
        }

    }

}
