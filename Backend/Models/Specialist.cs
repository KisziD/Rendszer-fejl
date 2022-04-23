using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Specialist")]
    public class Specialist
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Qualification { get; set; }
        public int UserID { get; set; }
    }

    public class NewSpecialist
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string SpecialityName { get; set; }
    }
}
