using RateForProfessor.Entities;
using RateForProfessor.Models;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IRateUniversityRepository 
    {
        public List<RateUniversityEntity> GetRateUniversitysByUniversityId(int universityId);
        public List<RateUniversityEntity> GetRateUniversityByStudentId(int studentId);


        //List<UniversityOverallRating> GetOverallRatingUniversities();

    }
}
