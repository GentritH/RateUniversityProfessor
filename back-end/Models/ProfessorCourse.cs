using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class ProfessorCourse
    {
        public int ProfessorCourseId { get; set; } 

        public int ProfessorId { get; set; } 

        public int CourseId { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual Course Courses { get; set; }
        
    }
}


