using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AuthenticateUser(User user)
        {
            var userStored = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userStored != null)
            {
                return userStored;
            }

            return null;
        }


        public async Task<List<User>> GetUsersByRole(UserRoleType roleType)
        {
            return await _dbContext.Users
                .Join(
                    _dbContext.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { User = user, UserRole = userRole })
                .Where(u => u.UserRole.RoleId == (int)roleType)
                .Select(u => u.User)
                .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new Exception("User not found");
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name)
                ?? throw new Exception("User not found");
        }


        public async Task<int> GetStudentCount()
        {
            return await _dbContext.UserRoles
                .CountAsync(ur => ur.RoleId == (int)UserRoleType.Student);
        }


    }
}
