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

        [HttpGet("esp/{id}")]
        public IEnumerable<EligibleSpecialist> GetEligibleSpecialists(int id)
        {
            var query = context.Specialists
                .Join(
                    context.Specialities,
                    specialist => specialist.Qualification,
                    speciality => speciality.ID,
                    (specialist, speciality) => new
                    {
                        specialistID = specialist.ID,
                        categoryID = speciality.CategoryID,
                        specialistName = specialist.Name
                    });

            Maintenance m = context.Maintenances.Where(m => m.ID == id).FirstOrDefault();
            Device d = context.Devices.Where(d => d.ID == m.DeviceID).FirstOrDefault();
            Category c = context.Categories.Where(c => c.ID == d.CategoryID).FirstOrDefault();
            LinkedList<EligibleSpecialist> list = new LinkedList<EligibleSpecialist>();
            foreach(var item in query)
            {
                if (item.categoryID == c.ID)
                {
                    list.AddLast(new EligibleSpecialist() { ID = item.specialistID, Name = item.specialistName });
                }
            }
            return list;
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
