using System.Security.Cryptography;
using System.Text;
using WeBScraper_CourseProject_.Data;

namespace WeBScraper_CourseProject_
{
    public class Seed
    {
        public Seed()
        {

        }
        public static async Task SeedUsers(ApplicationDbContext context, IScraperService scraperService)
        {
            var allnews = await scraperService.GetParsedArticles();


            foreach (var news in allnews)
            {
                bool isExists = context.Articles.Any(x => x.Title == news.Title);

                if (!isExists)
                {
                    var art = new Article
                    {
                        Title = news.Title,
                        Img = news.Img,
                        Details = news.Details
                    };
                    await context.Articles.AddAsync(art);

                }
            }

            await context.SaveChangesAsync();
        }
    }
}
