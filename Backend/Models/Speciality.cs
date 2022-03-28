using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Speciality")]
    public class Speciality
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
}
