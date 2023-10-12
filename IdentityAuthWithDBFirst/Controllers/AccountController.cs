using IdentityAuthWithDBFirst.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityAuthWithDBFirst.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly ILogger<WeatherForecastController> _logger;

        public AccountController(ILogger<WeatherForecastController> logger,
            UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager,
            RoleManager<AspNetRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]        
        public async Task<IActionResult> Register(AspNetUser reqUser, string password)
        {
            //should have used another model object instead of AspNetUser

            var user = await _userManager.FindByEmailAsync(reqUser.Email);
            if(user != null) return Ok(new { status = false, message = "user already exists." });

            //for mapping it should have AutoMapper
            user = new AspNetUser()
                        {
                            FirstName = reqUser.FirstName,
                            LastName = reqUser.LastName,
                            Email = reqUser.Email,
                            EmailConfirmed = reqUser.EmailConfirmed,
                            UserName = reqUser.UserName
                        };

            var newusersres = await _userManager.CreateAsync(user, password);
            if(newusersres.Succeeded) return Ok(new { status = false, message = "failed to  create user." });

            return Ok(new { status = true, message = "user created successfully." });
        }

        [AllowAnonymous]
        [HttpPost("Login")]        
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var res = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return Ok(res);
        }

        [HttpPost("Logout")]        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
