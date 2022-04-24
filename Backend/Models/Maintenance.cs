using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum States
{
    Accepted,
    Denied,
    Started,
    Done,
    Pending
}

namespace Backend.Models
{
    [Table("Maintenance")]
    public class Maintenance
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int DeviceID { get; set; }
        public States State { get; set; }
        public string Justification { get; set; }
        public int Severity { get; set; }
    }

    public class NewMaintenance
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Justification { get; set; }
    }
}