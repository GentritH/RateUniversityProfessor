using AutoMapper;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Repositories;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
    private readonly IGenericRepository<DepartmentEntity> _departmentRepository;
    private readonly IGenericRepository<ProfessorEntity> _professorRepository;
    private readonly IGenericRepository<RateProfessorEntity> _rateProfessorRepository;
    private readonly IGenericRepository<RateUniversityEntity> _rateUniversityRepository;
    private readonly IGenericRepository<UniversityEntity> _universityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AdminDashboardService(
        IGenericRepository<DepartmentEntity> departmentRepository,
        IGenericRepository<ProfessorEntity> professorRepository,
        IGenericRepository<RateProfessorEntity> rateProfessorRepository,
        IGenericRepository<RateUniversityEntity> rateUniversityRepository,
        IGenericRepository<UniversityEntity> universityRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        _professorRepository = professorRepository ?? throw new ArgumentNullException(nameof(professorRepository));
        _rateProfessorRepository = rateProfessorRepository ?? throw new ArgumentNullException(nameof(rateProfessorRepository));
        _rateUniversityRepository = rateUniversityRepository ?? throw new ArgumentNullException(nameof(rateUniversityRepository));
        _universityRepository = universityRepository ?? throw new ArgumentNullException(nameof(universityRepository));
        _userRepository = userRepository;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

        public async Task<int> GetDepartmentCountAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var countDepartment = departments.Count();
            return countDepartment;
        }

        public async Task<RateProfessor> SortFromHighestRatedProfessorAsync()
        {
            var sortedEntity = (await _rateProfessorRepository.GetAllAsync()).OrderByDescending(rp => rp.Overall);
            var highestRatedProfessor = sortedEntity.FirstOrDefault();
            var highestRatedProfessorDto = _mapper.Map<RateProfessor>(highestRatedProfessor);
            return highestRatedProfessorDto;
        }

        public async Task<RateUniversity> GetHighestRatedUniversityAsync()
        {
            var highestRatedUniversityEntity = (await _rateUniversityRepository.GetAllAsync()).MaxBy(ru => ru.Overall);
            var highestRatedUniversity = _mapper.Map<RateUniversity>(highestRatedUniversityEntity);
            return highestRatedUniversity;
        }

        public async Task<University> GetOldestUniversityAsync()
        {
            var oldestUniversityEntity = (await _universityRepository.GetAllAsync()).OrderBy(u => u.EstablishedYear).FirstOrDefault();
            var oldestUniversity = _mapper.Map<University>(oldestUniversityEntity);
            return oldestUniversity;
        }

        public async Task<int> GetProfessorCountAsync()
        {
            var count = (await _professorRepository.GetAllAsync()).Count();
            return count;
        }

        public async Task<int> GetUniversityCountAsync()
        {
            var count = (await _universityRepository.GetAllAsync()).Count();
            return count;
        }
        public async Task<int> GetStudentCount()
        {
            return await _userRepository.GetStudentCount();
        }

    }
}
