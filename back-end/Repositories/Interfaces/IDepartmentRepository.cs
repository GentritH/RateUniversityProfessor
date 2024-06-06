using RateForProfessor.Entities;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        public List<DepartmentEntity> GetDepartmentsByUniversity(int universityId);
        public DepartmentEntity GetDepartmentByName(string name);
        
    }
}
