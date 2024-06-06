using AutoMapper;
using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Models;

namespace RateForProfessor.Mappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {

            CreateMap<Professor, ProfessorEntity>().ReverseMap();

            CreateMap<RateProfessor, RateProfessorEntity>().ReverseMap();

            CreateMap<Department, DepartmentEntity>().ReverseMap();

            CreateMap<Course, CourseEntity>().ReverseMap();

            CreateMap<News, NewsEntity>().ReverseMap();

            CreateMap<University, UniversityEntity>().ReverseMap();

            CreateMap<RateUniversity, RateUniversityEntity>().ReverseMap();

            CreateMap<User, UserDto>();

            CreateMap<Role, RoleDto>();

        }
    }
}
