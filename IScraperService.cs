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
    }
}
