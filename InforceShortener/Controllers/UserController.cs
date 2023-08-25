using InforceShortener.Data.Models;
using InforceShortener.Interfaces;
using InforceShortener.Models;
using Microsoft.AspNetCore.Mvc;

namespace InforceShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public UserController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginModel)
        {
            string token = _authorizationService.GetToken(loginModel.Username, loginModel.Password);

            if (token == null)
            {
                return BadRequest("Wrong username or password");
            }

            return Ok( new { token } );
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterDTO registerModel)
        {
            var user = new User() 
            { 
                Username = registerModel.Username, 
                Password = registerModel.Password, 
                UrlRecords = new List<UrlRecord>(), 
                Role = "User" 
            };

            _authorizationService.AddUser(user);

            string token = _authorizationService.GetToken(registerModel.Username, registerModel.Password);

            if (token == null)
            {
                return BadRequest("Something went wrong. Please, try again later");
            }

            return Ok( new { token } );
        }
    }
}
