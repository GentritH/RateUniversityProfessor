using RateForProfessor.Models;

namespace RateForProfessor.Services.Interfaces
{
    public interface INewsService
    {
        public List<News> GetAllNewsDescByDate();

        public News GetLatestCreatedNews();

        public List<News> GetThreeLatestCreatedNews();

        IEnumerable<DayNewsCountDto> GetRecentNewsCount();


        //public News CreateNews(News news, string photoPath);

        //public void UploadProfilePhoto(int studentId, string photoPath);
    }
}
