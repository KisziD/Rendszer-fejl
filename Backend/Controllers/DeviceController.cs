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
        public IEnumerable<NewDevice> get()
        {
            LinkedList<NewDevice> devices = new LinkedList<NewDevice>();
            Dictionary<int, string> categories = new Dictionary<int, string>();
            foreach(var category in context.Categories)
            {
                categories[category.ID] = category.Name;
            }
            foreach(var device in context.Devices)
            {
                NewDevice dev = new NewDevice();
                dev.Name = device.Name;
                dev.ID = device.ID;
                dev.Description = device.Description;
                dev.Location = device.Location;
                dev.CategoryName = categories[device.CategoryID];
                devices.AddLast(dev);
            }
            return devices;
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
            return "{\"name\":\"" + context.Devices.Where(s => s.ID == id).FirstOrDefault()?.Name + "\"}";
        }

        [HttpGet("all/{id}")]
        public Device? getById(int id)
        {
            return context.Devices.Where(s => s.ID == id).FirstOrDefault();
        }

        [HttpPost("add")]
        public string post([FromBody] NewDevice device)
        {
            Device dev = new Device();
            dev.ID = device.ID;
            dev.Name = device.Name;
            dev.Description = device.Description;
            dev.Location = device.Location;
            Category category = context.Categories.Where(c => c.Name == device.CategoryName).FirstOrDefault();
            if (category == null)
            {
                return "{\"response\":0}";
            }
            else
            {
                context.Devices.Where(d => d.ID == device.ID).FirstOrDefault().CategoryID = category.ID;
                context.SaveChanges();
                return "{\"response\":1}";
            }
        }

        [HttpPost("assigncategory")]
        public string assignCategory([FromBody] AssignCategory assignCategory)
        {
            Device device = context.Devices.Where(d => d.ID == assignCategory.deviceID).FirstOrDefault();
            Category category = context.Categories.Where(c => c.Name == assignCategory.categoryName).FirstOrDefault();
            if(device == null || category == null)
            {
                return "{\"response\":0}";
            }
            else
            { 
                context.Devices.Where(d => d.ID == assignCategory.deviceID).FirstOrDefault().CategoryID = category.ID;
                context.SaveChanges();
                return "{\"response\":1}";
            }
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            context.Devices.Remove(context.Devices.Where(s => s.ID == id).First());
            context.SaveChanges();
            return "{\"response\":0}";
        }

    }

}
