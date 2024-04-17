using Hackfest.Data;
using Hackfest.Models;
using Hackfest.Models.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackfest.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;
        public UsersController(JwtService jwtService, AppDbContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        // login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if(loginModel == null || _context.Users.FirstOrDefault(U => U.Email == loginModel.Email) is not ApplicationUser user)
            {
                return BadRequest(new
                {
                    message = "login model is required or the user not found!"
                });
            }

            return Ok(new
            {
                token = _jwtService.GenerateTokenAsync(user.Id)
            });
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp(SignUpModel signUpModel)
        {
            if(signUpModel == null)
            {
                return BadRequest(new
                {
                    message = "singUp model is required!"
                });
            }

            var user = new ApplicationUser
            {
                Email = signUpModel.Email,
                Password = signUpModel.Password,
                UserName = signUpModel.UserName
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "account created seccufuly!"
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Users.ToList());
        }

    }
}
