using RateForProfessor.Extensions;
using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IProfessorService
    {

        public List<Professor> SearchProfessors(Search search);
        public Professor CreateProfessor(Professor professor, string photoPath, int departmentId);

        Task<IEnumerable<Professor>> GetProfessorsByDepartmentIdAsync(int departmentId);


        //public void UploadProfilePhoto(int professorId, string photoPath);
    }
}
