using RateForProfessor.Extensions;
using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface IUniversityService
    {      
        public University GetUniversityByName(string name);
        public List<University> SearchUniversities(Search search);

    }
}
