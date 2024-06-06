using RateForProfessor.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Entities
{
    public class UniversityEntity
    {
        [Key]
        public int UniversityId { get; set; }

        public string Name { get; set; }

        public int EstablishedYear { get; set; }

        public string Description { get; set; }

        public int StaffNumber { get; set; }

        public int StudentsNumber { get; set; }

        public int CoursesNumber { get; set; }

        public string? ProfilePhotoPath { get; set; }

        public ICollection<DepartmentEntity> Departments { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<RateUniversityEntity> RateUniversities { get; set; }
    }
}
