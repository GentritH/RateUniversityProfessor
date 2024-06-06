using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Models;
using RateForProfessor.Services.Interfaces;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IGenericService<News, NewsEntity> _service;
        private readonly INewsService _newsService;

        public NewsController(IGenericService<News, NewsEntity> service, INewsService newsService)
        {
            _service = service;
            _newsService = newsService;
        }

        [HttpGet("GetAllNews")]
        public async Task<ActionResult<IEnumerable<News>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("GetNewsById/{id}")]
        public async Task<ActionResult<News>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpGet("GetAllNewsDescByDate")]
        public List<News> GetAllNewsDescByDate()
        {
            var result = _newsService.GetAllNewsDescByDate();
            return result;
        }

        [HttpGet("latestCreated")]
        public ActionResult<News> GetLatestCreatedNews()
        {
            var latestNews = _newsService.GetLatestCreatedNews();
            if (latestNews == null)
            {
                return NotFound();
            }
            return Ok(latestNews);
        }

        [HttpGet("threeLatestCreated")]
        public ActionResult<List<News>> GetThreeLatestCreatedNews()
        {
            var threeLatestNews = _newsService.GetThreeLatestCreatedNews();
            if (threeLatestNews == null || threeLatestNews.Count == 0)
            {
                return NotFound();
            }
            return Ok(threeLatestNews);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateWithPhoto([FromForm] News dto, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string photoPath = FileUploadHelper.SaveProfilePhoto(file);
            var createdEntity = await _service.AddWithPhotoAsync(dto, photoPath);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }


        [HttpPut("UpdateNews/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] News dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("DeleteNews/{id}")]
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

        [HttpGet("RecentNewsCount")]
        public ActionResult<IEnumerable<DayNewsCountDto>> GetRecentNewsCount()
        {
            var recentNewsCount = _newsService.GetRecentNewsCount();
            return Ok(recentNewsCount);
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
