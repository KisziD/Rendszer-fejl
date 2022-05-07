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

    public class NewSpeciality
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }

    public class SpecialityName
    {
        public SpecialityName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
