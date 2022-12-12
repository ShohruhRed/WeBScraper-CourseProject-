using Microsoft.AspNetCore.Mvc;

namespace WeBScraper_CourseProject_
{
    public interface IScraperService
    {
        public Task<List<Article>> BelarusNewsScraper();
        public Task<List<Article>> UzbekistanNewsScraper();
        public Task<List<Article>> SportNewsScraper();
        public Task<List<Article>> GamingNewsScraper();
        public Task<List<Article>> MusicNewsScraper();
        public Task<List<Article>> MovieNewsScraper();
        public Task<List<News>> GetDbNews();
        public Task<List<Article>> GetAllNews();
        public Task<string> AddCommentary(int newsId, string commentary);
        public Task<List<Commentary>> GetCommentaries(int newsId);
        public Task<string> AddNews(News news);
        public Task<List<News>> GetSingleNews(int id);
        public Task DeleteNews(int id);
        public Task<List<Article>> GetParsedArticles();
    }
}
