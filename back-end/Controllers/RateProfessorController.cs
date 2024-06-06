using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.ML.DataModels;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RateProfessorController : ControllerBase
    {
        private readonly IGenericService<RateProfessor, RateProfessorEntity> _service;
        private readonly PredictionEnginePool<SampleObservation, SamplePrediction> _predictionEnginePool;
        private readonly IRateProfessorService _rateProfessorService;

        public RateProfessorController
        (
            IGenericService<RateProfessor, RateProfessorEntity> service,
            PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool,
            IRateProfessorService rateProfessorService
        )
        {
            _service = service;
            _predictionEnginePool = predictionEnginePool;
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
            SampleObservation sampleData = new SampleObservation() { SentimentText = rateProfessorDto.Feedback };

            SamplePrediction prediction = _predictionEnginePool.Predict(sampleData);
            bool isToxic = prediction.IsToxic;
            float probability = CalculateMethods.CalculatePercentage(prediction.Score);
            string retVal = $"Prediction: Is Toxic?: '{isToxic.ToString()}' with {probability.ToString()}% probability of toxicity  for the text '{rateProfessorDto.Feedback}'";

            if (isToxic)
            {
                return Conflict(new { Status = "Toxic", Message = retVal });
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
