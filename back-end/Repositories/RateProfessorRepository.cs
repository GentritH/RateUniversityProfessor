using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class RateProfessorRepository : IRateProfessorRepository
    {
        private readonly AppDbContext _dbContext;

        public RateProfessorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RateProfessorEntity> GetRateProfessorsByProfessorId(int professorId)
        {
            return _dbContext.RateProfessors
                .Where(rp => rp.ProfessorId == professorId)
                .ToList();
        }

        public List<RateProfessorEntity> GetRateProfessorsByStudentId(int studentId)
        {
            return _dbContext.RateProfessors
                .Where(rp => rp.UserId == studentId)
                .ToList();
        }

    }
}
