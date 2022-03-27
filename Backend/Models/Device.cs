using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }

    public class AssignCategory
    {
        public int deviceID { get; set; }
        public string categoryName { get; set; }
    }
}
