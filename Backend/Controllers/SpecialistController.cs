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
        public string? name(int id)
        {
            return "{\"name\":\""+ context.Specialists.Where(s => s.ID == id).FirstOrDefault()?.Name+"\"}";
        }

        [HttpGet("all/{id}")]
        public Specialist? getById(int id) { 
            return context.Specialists.Where(s => s.ID == id).FirstOrDefault(); 
        }

        [HttpGet("tasks/{id}")]
        public IEnumerable<MaintenanceView> getTasks(int id)
        {
            List<MaintenanceView> tasks = new List<MaintenanceView>();
            var query = context.Tasks.Where(t => t.SpecialistID == id).ToArray();
            foreach(var item in query)
            {

                Maintenance m = context.Maintenances.FirstOrDefault(m => m.ID == item.MaintenanceID);
                Device d = context.Devices.Where(d => d.ID == m.DeviceID).FirstOrDefault();
                Category c = context.Categories.Where(c => c.ID == d.CategoryID).FirstOrDefault();
                tasks.Add(new MaintenanceView()
                {
                    ID = m.ID,
                    Date = m.Date.ToString().Split(" ")[0],
                    State = m.State.ToString(),
                    Device = d.Name + ": " + d.Location,
                    Instructions = c.Instructions,
                });
            }
            return tasks;
        }

        [HttpPost("add")]
        public string post([FromBody] NewSpecialist specialist)
        {

            User newUser = new User();

            newUser.Username = specialist.Username;
            newUser.Password = specialist.Password;
            newUser.Role = specialist.Role;

            context.Users.Add(newUser);
            context.SaveChanges();

            Specialist newSpecialist = new Specialist();

            newSpecialist.Name = specialist.Name;
            newSpecialist.Qualification = context.Specialities.Where(s => s.Name == specialist.SpecialityName).FirstOrDefault()?.ID;
            newSpecialist.UserID = context.Users.Where(u => u.Username == specialist.Username).FirstOrDefault().ID;

            context.Specialists.Add(newSpecialist);
            context.SaveChanges();
            return "{\"response\":0}";
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Specialists.Remove(context.Specialists.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
