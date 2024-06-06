using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RateForProfessor.Entities.Identity;

namespace RateForProfessor.Entities
{
    public class RateProfessorEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int Overall { get; set; }

        public int CommunicationSkills { get; set; }

        public int Responsiveness { get; set; }

        public int GradingFairness { get; set; }

        public string Feedback { get; set; }

        public virtual ProfessorEntity Professor { get; set; }

        public virtual User User { get; set; }
    }
}
