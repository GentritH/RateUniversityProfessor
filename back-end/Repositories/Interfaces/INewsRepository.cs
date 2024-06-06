using RateForProfessor.Entities;
using RateForProfessor.Extensions;
using RateForProfessor.Models;

namespace RateForProfessor.Repositories.Interfaces
{
    public interface INewsRepository
    {
        public List<NewsEntity> GetAllNewsDescByDate();

        public NewsEntity GetLatestCreatedNews();

        public List<NewsEntity> GetThreeLatestCreatedNews();

        List<DayNewsCountDto> GetNewsCounts(DateTime startDate);


    }
}
