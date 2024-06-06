using AutoMapper;
using RateForProfessor.Models;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

namespace RateForProfessor.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }


        public List<News> GetAllNewsDescByDate()
        {
            var newsEntities = _newsRepository.GetAllNewsDescByDate();
            var news = _mapper.Map<List<News>>(newsEntities);
            return news;
        }

        public News GetLatestCreatedNews()
        {
            var newsEntities = _newsRepository.GetLatestCreatedNews();
            var news = _mapper.Map<News>(newsEntities);
            return news;
        }

        public List<News> GetThreeLatestCreatedNews()
        {
            var newsEntities = _newsRepository.GetThreeLatestCreatedNews();
            var news = _mapper.Map<List<News>>(newsEntities);
            return news;
        }

        public IEnumerable<DayNewsCountDto> GetRecentNewsCount()
        {
            var lastWeekStart = DateTime.Today.AddDays(-7);
            var newsCounts = _newsRepository.GetNewsCounts(lastWeekStart);

            var currentDate = DateTime.Today;
            for (int i = 0; i < 7; i++)
            {
                if (!newsCounts.Any(nc => nc.Date.Date == currentDate.AddDays(-i).Date))
                {
                    newsCounts.Add(new DayNewsCountDto
                    {
                        Date = currentDate.AddDays(-i),
                        Count = 0
                    });
                }
            }
            return newsCounts.OrderBy(nc => nc.Date).ToList();
        }




    }
}
