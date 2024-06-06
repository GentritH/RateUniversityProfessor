using RateForProfessor.Context;
using RateForProfessor.Entities;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _dbContext;

        public NewsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<NewsEntity> GetAllNewsDescByDate()
        {
            var news = _dbContext.News.OrderByDescending(n => n.PublicationDate).ToList();
            return news;
        }


        public NewsEntity GetLatestCreatedNews()
        {
            var latestNews = _dbContext.News.OrderByDescending(n => n.PublicationDate).FirstOrDefault();

            return latestNews;
        }

        public List<NewsEntity> GetThreeLatestCreatedNews()
        {
            var latestNews = _dbContext.News.OrderByDescending(n => n.PublicationDate).Take(3).ToList();

            return latestNews;
        }

        public List<DayNewsCountDto> GetNewsCounts(DateTime startDate)
        {
            return _dbContext.News
                .Where(news => news.PublicationDate >= startDate)
                .GroupBy(news => news.PublicationDate.Date)
                .Select(group => new DayNewsCountDto
                {
                    Date = group.Key,
                    Count = group.Count()
                })
                .ToList();
        }

    }
}
