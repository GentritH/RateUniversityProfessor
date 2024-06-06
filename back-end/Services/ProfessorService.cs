using AutoMapper;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Repositories;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        public List<Professor> SearchProfessors(Search search)
        {
            var professorEntities = _professorRepository.SearchProfessors(search);
            var professors = _mapper.Map<List<Professor>>(professorEntities);
            return professors;
        }

        public Professor CreateProfessor(Professor professor, string photoPath, int departmentId)
        {
            try
            {
                var professorEntity = _mapper.Map<ProfessorEntity>(professor);
                var result = _professorRepository.CreateProfessor(professorEntity, photoPath, departmentId);

                var professorCreated = _mapper.Map<Professor>(result);
                return professorCreated;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Professor>> GetProfessorsByDepartmentIdAsync(int departmentId)
        {
            var professors = await _professorRepository.GetProfessorsByDepartmentIdAsync(departmentId);
            return _mapper.Map<IEnumerable<Professor>>(professors);
        }

    }
}
