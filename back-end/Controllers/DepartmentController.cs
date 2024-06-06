using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericService<Department, DepartmentEntity> _service;
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IGenericService<Department, DepartmentEntity> service, IDepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("GetDepartmentsByUniversity/{id}")]
        public List<Department> GetDepartmentsByUniversity(int id)
        {
            return _departmentService.GetDepartmentsByUniversity(id);
        }

        [HttpGet("GetDepartmentByName/{name}")]
        public Department GetDepartmentByName(string name)
        {
            return _departmentService.GetDepartmentByName(name);
        }

        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> Create([FromBody] Department dto)
        {
            var createdEntity = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.DepartmentId }, createdEntity);
        }


        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department dto)
        {
            if (id != dto.DepartmentId)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("DeleteDepartment/{id}")]
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
