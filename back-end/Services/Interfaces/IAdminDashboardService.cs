using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<int> GetDepartmentCountAsync();
        Task<RateProfessor> SortFromHighestRatedProfessorAsync();
        Task<RateUniversity> GetHighestRatedUniversityAsync();
        Task<University> GetOldestUniversityAsync();
        Task<int> GetProfessorCountAsync();
        Task<int> GetUniversityCountAsync();
        Task<int> GetStudentCount();



        //Task<Student> GetStudentWithMostRatingsAsync();

    }
}
