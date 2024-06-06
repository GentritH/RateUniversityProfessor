using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Services;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IGenericService<University, UniversityEntity> _service;
        private readonly IUniversityService _universityService;

        public UniversityController(IGenericService<University, UniversityEntity> service, IUniversityService universityService)
        {
            _service = service;
            _universityService = universityService;
        }


        [HttpGet("GetAllUniversities")]
        public async Task<ActionResult<IEnumerable<University>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetUniversityById/{id}")]
        public async Task<ActionResult<University>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }


        [HttpPost("CreateUniversity")]
        public async Task<IActionResult> CreateWithPhoto([FromForm] University dto, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string photoPath = FileUploadHelper.SaveProfilePhoto(file);
            var createdEntity = await _service.AddWithPhotoAsync(dto, photoPath);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.UniversityId }, createdEntity);
        }



        [HttpPut("UpdateUniversity/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] University dto)
        {
            if (id != dto.UniversityId)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("DeleteUniversity/{id}")]
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

        [HttpPost("UploadProfilePhoto/{universityId}")]
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

        [HttpGet("GetUniversityByName/{name}")]
        public University GetUniversityByName(string name)
        {
            var university = _universityService.GetUniversityByName(name);
            return university;
        }

        [HttpGet("SearchUniversity")]
        public List<University> SearchUniversities([FromQuery] Search search)
        {
            var result = _universityService.SearchUniversities(search);
            return result;

        }



    }
}

