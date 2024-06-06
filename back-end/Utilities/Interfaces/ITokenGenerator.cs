using RateForProfessor.Entities.Identity;

namespace RateForProfessor.Utilities.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
