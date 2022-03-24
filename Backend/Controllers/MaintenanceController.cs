using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {

        private readonly DatabaseContext context;

        public MaintenanceController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Maintenance> get()
        {
            return context.Maintenances;
        }

        [HttpGet("instructions/{id}")]
        public string? instruction(int id)
        {
            return context.Maintenances.Where(s => s.ID == id).FirstOrDefault()?.Instructions;
        }

        [HttpGet("all/{id}")]
        public Maintenance? getById(int id)
        {
            return context.Maintenances.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public int post([FromBody] Maintenance maintenance)
        {
            context.Maintenances.Add(maintenance);
            context.SaveChanges();
            return 0;
        }

        [HttpDelete("{id}")]
        public int delete(int id)
        {
            context.Maintenances.Remove(context.Maintenances.Where(s => s.ID == id).First());
            context.SaveChanges();
            return 0;
        }

    }

}
