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
        public int post([FromBody] Models.Task tasks)
        {
            context.Tasks.Add(tasks);
            context.SaveChanges();
            return 0;
        }

        [HttpDelete("{id}")]
        public int delete(int id)
        {
            context.Tasks.Remove(context.Tasks.Where(s => s.ID == id).First());
            context.SaveChanges();
            return 0;
        }

    }

}
