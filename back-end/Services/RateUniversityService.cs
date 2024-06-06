using AutoMapper;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Repositories;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class RateUniversityService : IRateUniversityService
    {
        public readonly IRateUniversityRepository _rateUniversityRepository;
        public readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public RateUniversityService(IRateUniversityRepository rateUniversityRepository, IMapper mapper, IUniversityRepository universityRepository)
        {
            _rateUniversityRepository = rateUniversityRepository;
            _mapper = mapper;
            _universityRepository = universityRepository;
        }

        public List<RateUniversity> GetRateUniversityByUniversiyId(int universityId)
        {
            var rateUniversity = _rateUniversityRepository.GetRateUniversitysByUniversityId(universityId);
            return _mapper.Map<List<RateUniversity>>(rateUniversity);
        }

        public List<RateUniversity> GetRateUniversityByStudentId(int studentId)
        {
            var rateUniversity = _rateUniversityRepository.GetRateUniversityByStudentId(studentId);
            return _mapper.Map<List<RateUniversity>>(rateUniversity);
        }

        public async Task<UniversityOverallRating> OverallRatingByUniversityIdAsync(int universityId)
        {
            var university = await _universityRepository.GetUniversityWithRatingsAsync(universityId);

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
        }





        /*        public List<UniversityOverallRating> GetOverallRatingUniversities()
                {
                    var universityRatings = _rateUniversityRepository.GetAllRateUniversity()
                    .Join(
                            _universityRepository.GetAllUniversites(),
                            rateUniversity => rateUniversity.UniversityId,
                            university => university.UniversityId,
                            (rateUniversity, university) => new UniversityOverallRating
                            {
                                UniversityId = rateUniversity.UniversityId,
                                UniversityName = university.Name,
                                OverallRating = (int)rateUniversity.Overall
                            }
                        )
                        .GroupBy(r => r.UniversityId)
                        .Select(g => new UniversityOverallRating
                        {
                            UniversityId = g.Key,
                            UniversityName = g.First().UniversityName,
                            OverallRating = (int)g.Average(r => r.OverallRating),
                            TotalRatings = g.Count()
                        })
                        .ToList();

                    return universityRatings;
                }*/
    }
}
