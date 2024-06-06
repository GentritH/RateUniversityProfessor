using RateForProfessor.Entities;
using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IDepartmentService
    {
        public List<Department> GetDepartmentsByUniversity(int universityId);

        public Department GetDepartmentByName(string name);
    }
}
