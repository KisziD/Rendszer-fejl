using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Statechange")]
    public class Statechange
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public States State { get; set; }
        public int MaintenanceID { get; set; }
        public int SpecialistID { get; set; }
    }
}
