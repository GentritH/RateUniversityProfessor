using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class RateProfessor
    {
        public int Id { get; set; }

        public int ProfessorId { get; set; }

        public int UserId { get; set; }

        public int Overall { get; set; } = 0;

        public int CommunicationSkills { get; set; }

        public int Responsiveness { get; set; }

        public int GradingFairness { get; set; }

        public string? Feedback { get; set; }

    }
}
