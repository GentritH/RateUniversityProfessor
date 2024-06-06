using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IRateProfessorService
    {
        //List<RateProfessor> GetOverallRatingProfessors();
        //public List<object> GetOverallRatingProfessors();

        //public List<ProfessorOverallRating> GetOverallRatingProfessors();

        List<RateProfessor> GetRateProfessorsByProfessorId(int professorId);

        List<RateProfessor> GetRateProfessorsByStudentId(int studentId);

        public double CalculateOverall(int communicationskills, int responsiveness, int gradingfairness);
    }
}
