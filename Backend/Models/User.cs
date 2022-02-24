using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string username { get; set; }
        public string Password { get; set; }
    }
}
