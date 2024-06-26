using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RateForProfessor.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RateProfessorController : ControllerBase
    {
        private readonly IGenericService<RateProfessor, RateProfessorEntity> _service;
        private readonly IPredictionService _predictionService;
        private readonly IRateProfessorService _rateProfessorService;

        public RateProfessorController
        (
            IGenericService<RateProfessor, RateProfessorEntity> service,
            IPredictionService predictionService,
            IRateProfessorService rateProfessorService
        )
        {
            _service = service;
            _predictionService = predictionService;
            _rateProfessorService = rateProfessorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateProfessor>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetRateProfessorById/{id}")]
        public async Task<ActionResult<RateProfessor>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("RateProfessors/Professor/{professorId}")]
        public ActionResult<List<RateProfessor>> GetRateProfessorsByProfessorId(int professorId)
        {
            var rateProfessors = _rateProfessorService.GetRateProfessorsByProfessorId(professorId);
            return Ok(rateProfessors);
        }

        [HttpGet("RateProfessors/Student/{studentId}")]
        public ActionResult<List<RateProfessor>> GetRateProfessorsByStudentId(int studentId)
        {
            var rateProfessors = _rateProfessorService.GetRateProfessorsByStudentId(studentId);
            return Ok(rateProfessors);
        }



        [HttpPost("CreateRateProfessor")]
        public async Task<IActionResult> Create([FromBody] RateProfessor rateProfessorDto)
        {
            var predictionResult = await _predictionService.PredictToxicityAsync(rateProfessorDto.Feedback);

            if (predictionResult.IsToxic)
            {
                return Conflict(new { Status = "Toxic", Message = predictionResult.Message });
            }

            var createdEntity = await _service.AddAsync(rateProfessorDto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }


        [HttpPut("UpdateRateProfessor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RateProfessor dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("DeleteRateProfessor/{id}")]
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
