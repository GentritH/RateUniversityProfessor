using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _dbContext;

        public ProfessorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProfessorEntity GetProfessorByName(string name)
        {
            var professor = _dbContext.Profesors.FirstOrDefault(p => p.FirstName == name);
            return professor;
        }


        public List<ProfessorEntity> SearchProfessors(Search search)
        {
            var query = _dbContext.Profesors.Search(search.SearchTerm).AsQueryable();
            var professors = query.ToList();
            return professors;
        }

        public ProfessorEntity CreateProfessor(ProfessorEntity professor, string photoPath, int departmentId)
        {
            professor.ProfilePhotoPath = photoPath;
            _dbContext.Profesors.Add(professor);
            _dbContext.SaveChanges();

            var departmentProfessor = new DepartmentProfessorEntity
            {
                ProfessorId = professor.ProfessorId,
                DepartmentId = departmentId
            };
            _dbContext.DepartmentProfessors.Add(departmentProfessor);
            _dbContext.SaveChanges();

            return professor;
        }

        public async Task<IEnumerable<ProfessorEntity>> GetProfessorsByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.DepartmentProfessors
                                 .Where(dp => dp.DepartmentId == departmentId)
                                 .Select(dp => dp.Professor)
                                 .ToListAsync();
        }



    }
}
