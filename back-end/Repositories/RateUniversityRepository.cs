using Microsoft.EntityFrameworkCore;
using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class RateUniversityRepository : IRateUniversityRepository
    {
        private readonly AppDbContext _dbContext;

        public RateUniversityRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }


        public List<RateUniversityEntity> GetRateUniversitysByUniversityId(int universityId)
        {
            return _dbContext.RateUniversities
                .Where(ru => ru.UniversityId == universityId)
                .ToList();
        }

        public List<RateUniversityEntity> GetRateUniversityByStudentId(int studentId)
        {
            return _dbContext.RateUniversities
                .Where(ru => ru.UserId == studentId)
                .ToList();
        }


        /*        public async Task<UniversityOverallRating> OverallRatingByUniversityId(int universityId)
                {
                    var university = await _dbContext.Universities
                        .Include(u => u.RateUniversities)
                        .FirstOrDefaultAsync(u => u.UniversityId == universityId);

                    if (university == null)
                    {
                        return null; 
                    }

                    double overallRating = university.RateUniversities.Any()
                        ? university.RateUniversities.Average(r => r.Overall)
                        : 0;

                    int totalRatings = university.RateUniversities.Count();

                    return new UniversityOverallRating
                    {
                        UniversityId = university.UniversityId,
                        UniversityName = university.Name,
                        OverallRating = overallRating,
                        TotalRatings = totalRatings
                    };
                }*/
    }
}
