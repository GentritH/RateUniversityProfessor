using RateForProfessor.Entities;
using RateForProfessor.Extensions;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IUniversityRepository
    {
        public UniversityEntity GetUniversityByName(string name);
      
        public List<UniversityEntity> SearchUniversities(Search search);

        Task<UniversityEntity> GetUniversityWithRatingsAsync(int universityId);

    }
}
