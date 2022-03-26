using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly DatabaseContext context;

        public TaskController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Models.Task> get()
        {
            return context.Tasks;
        }

        [HttpPost("add")]
        public string post([FromBody] Models.Task tasks)
        {
            context.Tasks.Add(tasks);
            context.SaveChanges();
            return "{\"response\":0}";
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Tasks.Remove(context.Tasks.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
