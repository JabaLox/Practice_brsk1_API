using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usr = PracticStoreContext._context.Users.FirstOrDefault(x => x.LoginUser == user.LoginUser && x.PasswordUser == user.PasswordUser);

            if (usr == null)
            {
                return BadRequest();
            }
            var token = GenerateJwtToken(usr);
            return Ok(token);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            user.IdUser = PracticStoreContext._context.Users.Max(x => x.IdUser) + 1;
            user.RoleUser = 1;

            try
            {

                PracticStoreContext._context.Users.Add(user);
                PracticStoreContext._context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }


            return Ok(true);
        }

        [Authorize]
        [HttpGet("list")]
        public ActionResult ListUser(int IdUser)
        {
            var list = PracticStoreContext._context.Users.Include(x=>x.RoleUserNavigation);
            return Ok(list);
        }

        [Authorize]
        [HttpGet("info/{Id}")]
        public ActionResult GetUser(int Id)
        {
            var usr = PracticStoreContext._context.Users.Include(x=>x.RoleUserNavigation).FirstOrDefault(x => x.IdUser == Id);

            return Ok(usr);
        }

        [Authorize]
        [HttpPut("edit")]
        public ActionResult EditUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usr = PracticStoreContext._context.Users.FirstOrDefault(x => x.IdUser == user.IdUser);

            if (usr == null)
                return BadRequest();

            usr.IdUser = user.IdUser;
            usr.RoleUser = user.RoleUser;
            usr.NameUser = user.NameUser;
            usr.LoginUser = user.LoginUser;
            usr.PasswordUser = user.PasswordUser;

            try
            {
                PracticStoreContext._context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(true);
        }

        [NonAction]
        public User GetCurrectUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    IdUser = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    LoginUser = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,

                };
            }
            return null;
        }


        [NonAction]
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()), // Преобразование user.Id в строку
                new Claim(ClaimTypes.Name, user.NameUser),
                new Claim(ClaimTypes.Email, user.LoginUser),
                new Claim(ClaimTypes.Role, user.RoleUser.ToString())
            };
            var token = new JwtSecurityToken(
                _config.GetSection("Jwt:Issuer").Value, _config.GetSection("Jwt:Audience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(59),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
