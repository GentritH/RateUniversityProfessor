namespace RateForProfessor.Models
{
    public class UserDto : AbstractDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int UniversityId { get; set; }

        public int DepartmentID { get; set; }

        public int Grade { get; set; }

        public string? ProfilePhotoPath { get; set; }

        public RoleDto Role { get; set; }


    }
}
