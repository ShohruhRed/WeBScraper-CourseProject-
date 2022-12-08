using Microsoft.AspNetCore.Mvc;

namespace WeBScraper_CourseProject_
{
    public interface IScraperService
    {
        public Task<List<News>> BelarusNewsScraper();
        public Task<List<News>> UzbekistanNewsScraper();
        public Task<List<News>> SportNewsScraper();
        public Task<List<News>> GamingNewsScraper();
        public Task<List<News>> MusicNewsScraper();
        public Task<List<News>> MovieNewsScraper();
        public Task<List<News>> GetDbNews();
        public Task<List<News>> GetAllNews();
        public Task<string> AddNews(News news);
    }
}
