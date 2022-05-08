using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WeBScraper_CourseProject_.Data;

namespace WeBScraper_CourseProject_
{
    public class ScraperService
    {
        private readonly ApplicationDbContext _context;

        public ScraperService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<News>> BelarusNewsScrapping()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();

            var doc = await web.LoadFromWebAsync("https://www.belnovosti.by/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='ex_box']"))
            {

                string title = item.SelectSingleNode(".//span[@itemprop='name']").InnerText.Trim();
                string details = "https://www.belnovosti.by/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                jobs.Add(new News()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                });
            }

            return jobs;
        }
               

        public async Task<ActionResult<List<News>>> AddNews(News news)
        {
            try
            {
                await _context.NewsDb.AddAsync(news);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return await _context.NewsDb.ToListAsync();

        }

    }
}
