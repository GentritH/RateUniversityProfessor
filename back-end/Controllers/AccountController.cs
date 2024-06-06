using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using System.Security.Claims;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerFactory loggerFactory,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Invalid email or password." });

            _logger.LogInformation($"User found: {user.Email}");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return BadRequest("Your account was disabled");

                return BadRequest("Invalid login attempt.");
            }

            var token = await _userService.AuthenticateUser(user);
            if (token == null)
            {
                return Unauthorized(new { Message = "Authentication failed." });
            }

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            var roles = await _userService.GetCurrentUserRoleNamesAsync(claimsPrincipal);
            _logger.LogInformation($"User roles: {string.Join(", ", roles)}");

            await _userManager.UpdateAsync(user);
            _logger.LogInformation("User logged in.");

            return Ok(new { Token = token, UserId = user.Id, Email = user.Email, user, roles });
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromForm] RegisterDto model, IFormFile file)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string photoPath = FileUploadHelper.SaveProfilePhoto(file);

            var user = new User { UserName = model.UserName, Email = model.Email, Name = model.Name,
                Surname = model.Surname, UniversityId = model.UniversityId, DepartmentID = model.DepartmentID, Grade = model.Grade, ProfilePhotoPath =  photoPath};

            if (model.Email != "user@gmail.com" && model.Email != "user2@gmail.com" && model.Email != "user3@gmail.com" && model.Email != "gentrit.halimi@starlabspro.com" )
            {
                var existingEmail = await _userManager.FindByEmailAsync(model.Email);

                if (existingEmail != null)
                    return BadRequest("Email already exists");
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoleType.Student.ToString("G"));

                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(3, "User created a new account with password.");
                return Ok("Registration successful");
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors);
        }


    }
}


