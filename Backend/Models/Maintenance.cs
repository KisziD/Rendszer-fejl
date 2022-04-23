using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum States
{
    Accepted,
    Denied,
    Started,
    Done
}

namespace Backend.Models
{
    [Table("Maintenance")]
    public class Maintenance
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Norm_h { get; set; }
        public States State { get; set; }
        public string Justification { get; set; }
        public int Severity { get; set; }
    }
}