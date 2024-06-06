using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Models;
using System.ComponentModel.DataAnnotations;
using static RateForProfessor.Entities.Identity.RoleConfiguration;

namespace RateForProfessor.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<DepartmentProfessorEntity> DepartmentProfessors { get; set; }
        public DbSet<ProfessorCourseEntity> ProfessorCourses { get; set; }
        public DbSet<ProfessorEntity> Profesors { get; set; }
        public DbSet<RateProfessorEntity> RateProfessors { get; set; }
        public DbSet<RateUniversityEntity> RateUniversities { get; set; }
        public DbSet<UniversityEntity> Universities { get; set; }
        public DbSet<NewsEntity> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleSeedData());
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserToken>().ToTable("UserTokens");




            modelBuilder.Entity<CourseEntity>().ToTable("Courses");
            modelBuilder.Entity<DepartmentEntity>().ToTable("Departments");
            modelBuilder.Entity<DepartmentProfessorEntity>().ToTable("DepartmentProfessors");
            modelBuilder.Entity<ProfessorCourseEntity>().ToTable("ProfessorCourses");
            modelBuilder.Entity<ProfessorEntity>().ToTable("Profesors");
            modelBuilder.Entity<RateProfessorEntity>().ToTable("RateProfessors");
            modelBuilder.Entity<RateUniversityEntity>().ToTable("RateUniversities");
            modelBuilder.Entity<UniversityEntity>().ToTable("Universities");
            modelBuilder.Entity<NewsEntity>().ToTable("News");



            modelBuilder.Entity<CourseEntity>()
               .HasKey(pk => new { pk.ID });

            modelBuilder.Entity<CourseEntity>()
                .HasOne(ae => ae.Department)
                .WithMany(c => c.Courses)
                .HasForeignKey(ae => ae.DepartmentID);




            modelBuilder.Entity<DepartmentProfessorEntity>()
                .HasKey(dp => new { dp.DepartmentId, dp.ProfessorId });

            modelBuilder.Entity<DepartmentProfessorEntity>()
                .HasOne(dp => dp.Department)
                .WithMany(ae => ae.DepartmentProfessors)
                .HasForeignKey(dp => dp.DepartmentId);

            modelBuilder.Entity<DepartmentProfessorEntity>()
                .HasOne(ae => ae.Professor)
                .WithMany(ae => ae.DepartmentProfessors)
                .HasForeignKey(fk => fk.ProfessorId);


            modelBuilder.Entity<ProfessorCourseEntity>()
                .HasKey(dp => new { dp.ProfessorId, dp.CourseId });


            modelBuilder.Entity<ProfessorCourseEntity>()
                .HasOne(ae => ae.Professor)
                .WithMany(ae => ae.ProfessorCourses)
                .HasForeignKey(fk => fk.ProfessorId);

            modelBuilder.Entity<ProfessorCourseEntity>()
                .HasOne(ae => ae.Course)
                .WithMany(ae => ae.ProfessorCourses)
                .HasForeignKey(fk => fk.CourseId);

            modelBuilder.Entity<DepartmentEntity>()
              .HasKey(pk => new { pk.DepartmentId });

            modelBuilder.Entity<DepartmentEntity>()
                .HasOne(ae => ae.University)
                .WithMany(ae => ae.Departments)
                .HasForeignKey(fk => fk.UniversityId);

            modelBuilder.Entity<ProfessorEntity>()
              .HasKey(pk => new { pk.ProfessorId });

            modelBuilder.Entity<RateProfessorEntity>()
             .HasKey(pk => new { pk.Id });

            modelBuilder.Entity<RateProfessorEntity>()
                .HasOne(ae => ae.Professor)
                .WithMany(ae => ae.RateProfessors)
                .HasForeignKey(fk => fk.ProfessorId);


            modelBuilder.Entity<RateProfessorEntity>()
                .HasOne(ae => ae.User)
                .WithMany(ae => ae.RateProfessors)
                .HasForeignKey(fk => fk.UserId);


            modelBuilder.Entity<RateUniversityEntity>()
             .HasKey(pk => new { pk.Id });

            modelBuilder.Entity<RateUniversityEntity>()
                .HasOne(ae => ae.University)
                .WithMany(ae => ae.RateUniversities)
                .HasForeignKey(fk => fk.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<RateUniversityEntity>()
                .HasOne(ae => ae.User)
                .WithOne(ae => ae.RateUniversity)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UniversityEntity>()
             .HasKey(pk => new { pk.UniversityId });

        }
    }
}
