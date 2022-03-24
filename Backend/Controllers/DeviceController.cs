using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly DatabaseContext context;

        public DeviceController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<Device> get()
        {
            return context.Devices;
        }

        [HttpGet("names")]
        public IEnumerable<string> names()
        {
            List<string> names = new List<string>();
            foreach (var device in context.Devices)
            {
                names.Add(device.Name);
            }
            return names;
        }

        [HttpGet("names/{id}")]
        public string name(int id)
        {
            return context.Devices.Where(s => s.ID == id).FirstOrDefault()?.Name;
        }

        [HttpGet("all/{id}")]
        public Device? getById(int id)
        {
            return context.Devices.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public int post([FromBody] Device devices)
        {
            context.Devices.Add(devices);
            context.SaveChanges();
            return 0;
        }

        [HttpDelete("{id}")]
        public int delete(int id)
        {
            context.Devices.Remove(context.Devices.Where(s => s.ID == id).First());
            context.SaveChanges();
            return 0;
        }

    }

}
