using Microsoft.AspNetCore.Mvc;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IProfessorRepository
    {
        public ProfessorEntity GetProfessorByName(string name);
        public List<ProfessorEntity> SearchProfessors(Search search);

        public ProfessorEntity CreateProfessor(ProfessorEntity professor, string photoPath, int departmentId);

        Task<IEnumerable<ProfessorEntity>> GetProfessorsByDepartmentIdAsync(int departmentId);


    }
}
