using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace RateForProfessor.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }


        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        public int Grade { get; set; }

        public string? ProfilePhotoPath { get; set; }

        public virtual UniversityEntity? University { get; set; }

        public DepartmentEntity? Department { get; set; }

        public RateUniversityEntity? RateUniversity { get; set; }

        public ICollection<RateProfessorEntity>? RateProfessors { get; set; }

    }


    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasOne(x => x.University)
                .WithMany()
                .HasForeignKey(x => x.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
