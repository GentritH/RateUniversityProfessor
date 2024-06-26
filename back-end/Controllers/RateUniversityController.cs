using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateUniversityController : ControllerBase
    {
        private readonly IGenericService<RateUniversity, RateUniversityEntity> _service;
        private readonly IRateUniversityService _rateUniversityService;
        private readonly IPredictionService _predictionService;


        public RateUniversityController
        (
            IGenericService<RateUniversity, RateUniversityEntity> service,
            IRateUniversityService rateUniversityService,
            IPredictionService predictionService
        )
        {
            _service = service;
            _rateUniversityService = rateUniversityService;
            _predictionService = predictionService;
        }


        [HttpPost("CreateRateUniversity")]
        public async Task<IActionResult> Create([FromBody] RateUniversity rateUniversityDto)
        {
            var predictionResult = await _predictionService.PredictToxicityAsync(rateUniversityDto.Feedback);

            if (predictionResult.IsToxic)
            {
                return Conflict(new { Status = "Toxic", Message = predictionResult.Message });
            }

            var createdEntity = await _service.AddAsync(rateUniversityDto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateUniversity>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("OverallRatingByUniversityId/{universityId}")]
        public async Task<IActionResult> OverallRatingByUniversityIdAsync(int universityId)
        {
            var universityRating = await _rateUniversityService.OverallRatingByUniversityIdAsync(universityId);

            if (universityRating == null)
            {
                return NotFound();
            }

            return Ok(universityRating);
        }

        [HttpGet("GetRateUniversityById/{id}")]
        public async Task<ActionResult<RateUniversity>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("RateUniversity/University/{universityId}")]
        public ActionResult<List<RateUniversity>> GetRateUniversityByUniversityId(int universityId)
        {
            var rateUniversity = _rateUniversityService.GetRateUniversityByUniversiyId(universityId);
            return Ok(rateUniversity);
        }

        [HttpGet("RateUniversity/Student/{studentId}")]
        public ActionResult<List<RateUniversity>> GetRateUniversityByStudentId(int studentId)
        {
            var rateUniversity = _rateUniversityService.GetRateUniversityByStudentId(studentId);
            return Ok(rateUniversity);
        }

        [HttpPut("UpdateRateUniversity/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RateUniversity dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("DeleteRateUniversity/{id}")]
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
