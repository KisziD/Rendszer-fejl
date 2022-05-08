using Microsoft.AspNetCore.Mvc;
using Backend.Contexts;
using Backend.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext context;

        public UserController(DatabaseContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToArray();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("token")]
        public string getToken([FromBody]TokenObject token)
        {
            User user = (User)context.Users.Where(u => u.ID == token.ID).FirstOrDefault();
            if ((user!=null)&&token.Token == user.Token)
            {
                return "{\"response\":1}";
            }
            else
            {
                return "{\"response\":0}";
            }
        }

        [HttpPost("login")]
        public string login([FromBody]LoginObject login)
        {
            User user = (User)context.Users.Where(u => u.Username == login.Username).FirstOrDefault();
            if ((user!=null)&&user.Password == login.Password)
            {
                int token = generateToken();
                user.Token = token;
                context.SaveChanges();
                return "{\"id\":"+user.ID+",\"token\":" + token + ":"+user.Role+ "}";
            }
            else
            {
                return "{\"id\":-1, \"token\":0}";
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        int generateToken()
        {
            Random random = new Random();
            int seed1 = random.Next(20000, 200000000);
            int seed2 = random.Next(20000, 200000000);
            return seed1 * seed2;
        }
    }
}
