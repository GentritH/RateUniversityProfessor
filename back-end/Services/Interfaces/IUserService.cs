using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;
using RateForProfessor.Models;
using System.Security.Claims;

namespace RateForProfessor.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User user);

        Task<List<UserDto>> GetUsersByRole(UserRoleType roleType);
        Task<UserDto> GetCurrentUserInfo(string userId);
        Task<List<string>> GetCurrentUserRoleNamesAsync(ClaimsPrincipal user);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByNameAsync(string name);


    }
}
