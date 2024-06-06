using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Entities
{
    public class ProfessorCourseEntity
    {

        [Key]
        [Column(Order = 0)]
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual ProfessorEntity Professor { get; set; }

        public virtual CourseEntity Course { get; set; }
    }
}

