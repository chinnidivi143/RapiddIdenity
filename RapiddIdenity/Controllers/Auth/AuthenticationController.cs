using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RapiddIdenity.Models;
using RapiddIdenity.Models.Authenication.SignUp;

namespace RapiddIdenity.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private readonly User<IdentityUser> _
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterUser registerUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(registerUser.Email);

                if (user == null)
                {
                    IdentityUser newUser = new()
                    {
                        UserName = registerUser.Username,
                        Email = registerUser.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };

                    var created = await _userManager.CreateAsync(newUser, registerUser.Password);
                    Roles role = registerUser.Role;

                    if (await _roleManager.RoleExistsAsync(role.ToString()))
                    {
                        await _userManager.AddToRoleAsync(newUser, role.ToString());
                    }

                    var token = _userManager.GenerateChangeEmailTokenAsync(newUser, registerUser.Email);
                    var msg = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = registerUser.Email });
                  //  MessageProcessingHandler 
                    return Ok(newUser);
                }
                else
                {
                    return StatusCode(500, "User already existed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var confirmEmail = _userManager.ConfirmEmailAsync(user, token);
                    if (confirmEmail.IsCompletedSuccessfully)
                    {

                    }
                }
            }
            catch
            {

            }

            return Ok();
        }
    }
}
