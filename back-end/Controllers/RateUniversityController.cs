using Microsoft.AspNetCore.Authorization;
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
    public class RateUniversityController : ControllerBase
    {
        private readonly IGenericService<RateUniversity, RateUniversityEntity> _service;
        private readonly IRateUniversityService _rateUniversityService;
        private readonly PredictionEnginePool<SampleObservation, SamplePrediction> _predictionEnginePool;

        public RateUniversityController
        (
            IGenericService<RateUniversity, RateUniversityEntity> service, 
            IRateUniversityService rateUniversityService,
            PredictionEnginePool<SampleObservation, SamplePrediction> predictionEnginePool
        )
        {
            _service = service;
            _rateUniversityService = rateUniversityService;
            _predictionEnginePool = predictionEnginePool;
        }


        [HttpPost("CreateRateUniversity")]
        public async Task<IActionResult> Create([FromBody] RateUniversity rateUniversityDto)
        {
            SampleObservation sampleData = new SampleObservation() { SentimentText = rateUniversityDto.Feedback };
            SamplePrediction prediction = _predictionEnginePool.Predict(sampleData);

            bool isToxic = prediction.IsToxic;
            float probability = CalculateMethods.CalculatePercentage(prediction.Score);
            string retVal = $"Prediction: Is Toxic?: '{isToxic.ToString()}' with {probability.ToString()}% probability of toxicity  for the text '{rateUniversityDto.Feedback}'";

            if (isToxic)
            {
                return Conflict(new { Status = "Toxic", Message = retVal });
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
