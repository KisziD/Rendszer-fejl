using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Norm_h { get; set; }
        public int Parent { get; set; }
    }
}
