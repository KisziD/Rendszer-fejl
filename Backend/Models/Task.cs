using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int ID { get; set; }
        public int MaintenanceID { get; set; }
        public int SpecialistID { get; set; }
    }
}
