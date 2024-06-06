using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(User user);

        Task<List<User>> GetUsersByRole(UserRoleType roleType);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByNameAsync(string name);
        Task<int> GetStudentCount();


    }
}
