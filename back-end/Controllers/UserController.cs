using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using System.Security.Claims;

namespace RateForProfessor.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<UserDto, User> _service;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IGenericService<UserDto, User> service, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _userService.GetUsersByRole(UserRoleType.Student);
            return Ok(students);
        }

        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _userService.GetUsersByRole(UserRoleType.Admin);
            return Ok(admins);
        }

        //[Authorize]
        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDto = await _userService.GetCurrentUserInfo(userId);
            return Ok(userDto);
        }

        [HttpGet("current/user/roles")]
        public async Task<IActionResult> GetCurrentUserRoleNames()
        {
            var currentUserRoleNames = await _userService.GetCurrentUserRoleNamesAsync(User);
            return Ok(currentUserRoleNames);
        }


        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            return user != null ? Ok(user) : NotFound(new { Message = "User not found", Email = email });
        }

        [HttpGet("GetUserByName/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            return user != null ? Ok(user) : NotFound(new { Message = "User not found", Name = name });
        }




        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }


        [HttpPost("UploadProfilePhoto/{id}")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile file)
        {
            try
            {
                var news = await _service.GetByIdAsync(id);
                if (news == null)
                {
                    return NotFound();
                }

                if (file != null)
                {
                    string photoPath = FileUploadHelper.SaveProfilePhoto(file);
                    await _service.UploadPhotoAsync(id, photoPath);
                    return Ok();
                }
                else
                {
                    return BadRequest("No file was uploaded.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the profile photo.");
            }
        }


    }
}
