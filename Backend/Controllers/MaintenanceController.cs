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

        [HttpGet("all/{id}")]
        public Maintenance? getById(int id)
        {
            return context.Maintenances.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public string post([FromBody] NewMaintenance maintenance)
        {
            Maintenance main = new Maintenance();
            main.Justification = maintenance.Justification;
            main.DeviceID = context.Devices.Where(d => d.Name == maintenance.Name && d.Location == maintenance.Location).FirstOrDefault().ID;
            main.State = States.Pending;
            main.Date = DateTime.Now;
            main.Severity = 0;
            context.Maintenances.Add(main);
            context.SaveChanges();
            return "{\"response\":0}";
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Maintenances.Remove(context.Maintenances.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
