using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Enums
{
    public enum UserRoleType : short
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "Student")]
        Student = 2,
    }
}
