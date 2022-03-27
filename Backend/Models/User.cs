using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Token { get; set; }
    }

    public class LoginObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenObject
    {
        public int ID { get; set; }
        public int Token { get; set; }
    }
}
