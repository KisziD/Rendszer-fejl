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
        public string Qualification { get; set; }
    }
}
