using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RateForProfessor.Entities;
using RateForProfessor.Entities.Identity;
using RateForProfessor.Enums;
using RateForProfessor.Models;
using RateForProfessor.Repositories;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;
using RateForProfessor.Utilities.Interfaces;
using System.Security.Claims;

namespace RateForProfessor.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;


        public UserService(IUserRepository userRepository, IMapper mapper, IGenericRepository<User> repository, UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> AuthenticateUser(User user)
        {
            var userAuth = await _userRepository.AuthenticateUser(user);
            if (userAuth == null)
            {
                return null;
            }

            var token = _tokenGenerator.GenerateToken(userAuth);
            return token;
        }


        public async Task<List<UserDto>> GetUsersByRole(UserRoleType roleType)
        {
            var users = await _userRepository.GetUsersByRole(roleType);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetCurrentUserInfo(string userId)
        {
            int userIdInt;
            int.TryParse(userId, out userIdInt);
            var user = await _repository.GetByIdAsync(userIdInt);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<string>> GetCurrentUserRoleNamesAsync(ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);

            return (List<string>)await _userManager.GetRolesAsync(currentUser);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(email);
            var user = _mapper.Map<UserDto>(userEntity);
            return user;
        }

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            var userEntity = await _userRepository.GetUserByNameAsync(name);
            var user = _mapper.Map<UserDto>(userEntity);
            return user;
        }

    }
}
