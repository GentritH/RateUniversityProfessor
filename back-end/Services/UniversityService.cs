using AutoMapper;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;
        public UniversityService(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        public University GetUniversityByName(string name)
        {
            var universityEntity = _universityRepository.GetUniversityByName(name);
            var university = _mapper.Map<University>(universityEntity);
            return university;
        }

        public List<University> SearchUniversities(Search search)
        {
            var universityEntities = _universityRepository.SearchUniversities(search);
            var universities = _mapper.Map<List<University>>(universityEntities);
            return universities;
        }
    }
}
