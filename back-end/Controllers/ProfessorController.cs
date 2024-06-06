using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using RateForProfessor.Validators;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly IGenericService<Professor, ProfessorEntity> _service;
        private readonly IProfessorService _professorService;

        public ProfessorController(IGenericService<Professor, ProfessorEntity> service, IProfessorService professorService)
        {
            _service = service;
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetAllProfessors()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetProfessorById/{id}")]
        public async Task<ActionResult<Professor>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("CreateProfessor/{departmentId}")]
        public IActionResult CreateProfessor(int departmentId, [FromForm] Professor professor, IFormFile file)
        {
            ProfessorValidator validator = new ProfessorValidator();
            var validationResult = validator.Validate(professor);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            string photoPath = FileUploadHelper.SaveProfilePhoto(file);
            var createdProfessor = _professorService.CreateProfessor(professor, photoPath, departmentId);
            return Ok(createdProfessor);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetProfessorsByDepartmentId(int departmentId)
        {
            var professors = await _professorService.GetProfessorsByDepartmentIdAsync(departmentId);
            if (!professors.Any())
            {
                return NotFound();
            }

            return Ok(professors);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateProfessor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Professor dto)
        {
            if (id != dto.ProfessorId)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProfessor/{id}")]
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

        [HttpPost("UploadProfilePhoto/{professorId}")]
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

        [HttpGet("SearchProfessor")]
        public List<Professor> SearchProfessors([FromQuery] Search search)
        {
            var result = _professorService.SearchProfessors(search);
            return result;

        }
    }
}
