using RateForProfessor.Entities;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IRateProfessorRepository
    {
        public List<RateProfessorEntity> GetRateProfessorsByProfessorId(int professorId);

        public List<RateProfessorEntity> GetRateProfessorsByStudentId(int studentId);
    }
}
