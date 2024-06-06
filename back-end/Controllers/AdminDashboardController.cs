using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Context;
using RateForProfessor.Models;
using RateForProfessor.Services;
using RateForProfessor.Services.Interfaces;
using RateForProfessor.Validators;

namespace RateForProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public AdminDashboardController(IAdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        [HttpGet("GetUniversityCount")]
        public async Task<ActionResult<int>> GetUniversityCountAsync()
        {
            return await _adminDashboardService.GetUniversityCountAsync();
        }

        [HttpGet("GetDepartmentCount")]
        public async Task<ActionResult<int>> GetDepartmentCountAsync()
        {
            return await _adminDashboardService.GetDepartmentCountAsync();
        }

        [HttpGet("GetProfessorCount")]
        public async Task<ActionResult<int>> GetProfessorCountAsync()
        {
            return await _adminDashboardService.GetProfessorCountAsync();
        }

        [HttpGet("GetStudentCount")]
        public async Task<IActionResult> GetStudentCount()
        {
            var count = await _adminDashboardService.GetStudentCount();
            return Ok(count);
        }


        [HttpGet("SortFromHighestRatedProfessor")]
        public async Task<ActionResult<RateProfessor>> GetHighestRatedProfessorAsync()
        {
            return await _adminDashboardService.SortFromHighestRatedProfessorAsync();
        }

        [HttpGet("GetHighestRatedUniversity")]
        public async Task<ActionResult<RateUniversity>> GetHighestRatedUniversityAsync()
        {
            return await _adminDashboardService.GetHighestRatedUniversityAsync();
        }

        [HttpGet("GetOldestUniversity")]
        public async Task<ActionResult<University>> GetOldestUniversityAsync()
        {
            return await _adminDashboardService.GetOldestUniversityAsync();
        }
    }
}