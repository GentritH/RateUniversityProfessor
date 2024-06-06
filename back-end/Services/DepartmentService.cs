using AutoMapper;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public List<Department> GetDepartmentsByUniversity(int universityId)
        {
            var departmentEntity = _departmentRepository.GetDepartmentsByUniversity(universityId);
            var departments = _mapper.Map<List<Department>>(departmentEntity);
            return departments;
        }

        public Department GetDepartmentByName(string name)
        {
            var departmentEntity = _departmentRepository.GetDepartmentByName(name);
            var department = _mapper.Map<Department>(departmentEntity);
            return department;
        }
    }
}
