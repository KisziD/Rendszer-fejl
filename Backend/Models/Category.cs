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
        public int? Interval { get; set; }
        public int? Parent { get; set; }
        public string? Instructions { get; set; }
    }

    public class CategoryName
    {
        public CategoryName(string _name)
        {
            Name = _name;
        }
        public string Name { get; set; }
    }

    public class NewCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Norm_h { get; set; }
        public int? Interval { get; set; }
        public string? Parent { get; set; }
        public string Instructions { get; set; }
    }
}
