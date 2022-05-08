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
        public IEnumerable<MaintenanceView> get()
        {
            LinkedList<MaintenanceView> maintenanceViews = new LinkedList<MaintenanceView>();
           LinkedList<Maintenance> maintenance = new LinkedList<Maintenance>();
            foreach(var m in context.Maintenances)
            {
                maintenance.AddLast(m);
            }
            foreach(var m in maintenance)
            {
                Device d = context.Devices.Where(d => d.ID == m.DeviceID).FirstOrDefault();
                MaintenanceView mv = new MaintenanceView();
                mv.ID = m.ID;
                mv.Date = m.Date.ToString().Split(" ")[0];
                mv.State = m.State.ToString();
                mv.Device = d.Name + ": " + d.Location;
                maintenanceViews.AddLast(mv);
            }

            return maintenanceViews;
        }

        [HttpGet("all/{id}")]
        public MaintenanceView? getById(int id)
        {

            Maintenance m = context.Maintenances.FirstOrDefault(m => m.ID == id);
            Device d = context.Devices.Where(d => d.ID == m.DeviceID).FirstOrDefault();
            Category c = context.Categories.Where(c => c.ID == d.CategoryID).FirstOrDefault();
            MaintenanceView mv = new MaintenanceView();
            mv.ID = m.ID;
            mv.Date = m.Date.ToString().Split(" ")[0];
            mv.State = m.State.ToString();
            mv.Device = d.Name + ": " + d.Location;
            mv.Instructions = c.Instructions;
            return mv;
        }

        [HttpPost("state")]
        public string setState([FromBody] setMaintenanceState newState)
        {
            context.Maintenances.Where(m => m.ID == newState.ID).FirstOrDefault().State = newState.State;
            if(newState.State == States.Denied)
            {
                context.Maintenances.Where(m => m.ID == newState.ID).FirstOrDefault().CancelReason = newState.Reason;
            }
            context.SaveChanges();
            context.Statechanges.Add(new Statechange()
            {
                Date = DateTime.Now,
                MaintenanceID = newState.ID,
                SpecialistID = newState.SpecialistID,
                State = newState.State
            });
            context.SaveChanges();
            return "{\"response\":0}";
        }

        [HttpPost("add")]
        public string post([FromBody] NewMaintenance maintenance)
        {
            Maintenance main = new Maintenance();
            main.Justification = maintenance.Justification;
            main.DeviceID = context.Devices.Where(d => d.Name == maintenance.Name && d.Location == maintenance.Location).FirstOrDefault().ID;
            main.Date = DateTime.Now;
            main.State = States.Pending;
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
