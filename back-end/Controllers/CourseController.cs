using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using RateForProfessor.Validators;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IGenericService<Course, CourseEntity> _service;

        public CourseController(IGenericService<Course, CourseEntity> service)
        {
            _service = service;
        }

        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> Create([FromBody] Course dto)
        {
            var createdEntity = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.ID }, createdEntity);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course dto)
        {
            if (id != dto.ID)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCourse/{id}")]
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
    }
}
