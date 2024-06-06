using RateForProfessor.Models;
using System.Security.Cryptography;

namespace RateForProfessor.Services.Interfaces
{
    public interface IRateUniversityService
    {

        List<RateUniversity> GetRateUniversityByUniversiyId(int universityId);
        List<RateUniversity> GetRateUniversityByStudentId( int studentId);
        Task<UniversityOverallRating> OverallRatingByUniversityIdAsync(int universityId);


        //public List<UniversityOverallRating> GetOverallRatingUniversities();
        //public List<UniversityOverallRating> GetOverallRatingUniversities();
        //Task<List<UniversityOverallRating>> GetOverallRatingUniversitiesAsync();


    }
}
