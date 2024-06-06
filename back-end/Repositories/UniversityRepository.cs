using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Extensions;
using RateForProfessor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RateForProfessor.Repositories
{
    public class UniversityRepository: IUniversityRepository
    {
        private readonly AppDbContext _dbContext;

        public UniversityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public UniversityEntity GetUniversityByName(string name)
        {
            var universiteti = _dbContext.Universities.FirstOrDefault(u => u.Name == name);
            return universiteti;
        }

        public List<UniversityEntity> SearchUniversities(Search search)
        {
            var query = _dbContext.Universities.SearchUniversity(search.SearchTerm).AsQueryable();
            var universities = query.ToList();
            return universities;
        }

        public async Task<UniversityEntity> GetUniversityWithRatingsAsync(int universityId)
        {
            return await _dbContext.Universities
                .Include(u => u.RateUniversities)
                .FirstOrDefaultAsync(u => u.UniversityId == universityId) ?? throw new InvalidOperationException($"University with ID {universityId} not found.");
        }
    }
}
