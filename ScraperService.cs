﻿using HtmlAgilityPack;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using System.Net;
using WeBScraper_CourseProject_.Data;
using WeBScraper_CourseProject_.Pages;

namespace WeBScraper_CourseProject_
{
    public class ScraperService : IScraperService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthenticationStateProvider _authenticationState;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public ScraperService(ApplicationDbContext context, AuthenticationStateProvider authenticationState,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authenticationState = authenticationState;
            _userManager = userManager;           
        }      

        [HttpGet]
        public async Task<List<News>> GetDbNews()
        {
            var authstate = await _authenticationState.GetAuthenticationStateAsync();
            var userAuth = authstate.User;
            var name = userAuth.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            


            var personNews = await _context.News_Users
                .Where(w => w.UserId == user.Id)
                .Include(n => n.News)
                .ToListAsync();

            var listNews = new List<News>();
          

            foreach (var item in personNews)
            {
                var list = new News
                {
                    Id = item.News.Id,
                    Title = item.News.Title,
                    Details = item.News.Details,
                    Img = item.News.Img
                };

                listNews.Add(list);
                
            }         
                       

            return listNews;

        }

        
        public async Task<string> AddNews(News news)
        {
            var authstate = await _authenticationState.GetAuthenticationStateAsync();
            var userAuth = authstate.User;
            var name = userAuth.Identity.Name;
            var user =  await _userManager.FindByNameAsync(name);
                       

            var usernews = new News_User
            {
                NewsId = news.Id,
                User = user,
                News = news,
                
            };

            try
            {
                await _context.NewsDb.AddAsync(news);
                await _context.News_Users.AddAsync(usernews);
                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "Ok";

        }

        public async Task<List<Article>> BelarusNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();

            var doc = await web.LoadFromWebAsync("https://www.belnovosti.by/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='ex_box']"))
            {

                string title = item.SelectSingleNode(".//span[@itemprop='name']").InnerText.Trim();
                string details = "https://www.belnovosti.by/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                });
            }

            return jobs;
        }      

        public async Task<List<Article>> UzbekistanNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.spot.uz/ru/news/");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='contentBox']"))
            {

                string title = item.SelectSingleNode(".//h2[@class='itemTitle']").InnerText.Trim();
                string details = "https://www.spot.uz/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = "https://www.spot.uz/" + item.SelectSingleNode(".//img").GetAttributeValue("data-src", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }

        public async Task<List<Article>> SportNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.mk.ru/sport/");
            foreach (var item in doc.DocumentNode.SelectNodes("//li[@class='article-listing__item']"))
            {

                string title = item.SelectSingleNode(".//h3[@class='listing-preview__title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                });
            }

            return jobs;
        }

        public async Task<List<Article>> GamingNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.playground.ru/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='post']"))
            {

                string title = item.SelectSingleNode(".//div[@class='post-title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }

        public async Task<List<Article>> MusicNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.mk.ru/culture/zd/");
            foreach (var item in doc.DocumentNode.SelectNodes("//li[@class='article-listing__item']"))
            {

                string title = item.SelectSingleNode(".//h3[@class='listing-preview__title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }

        public async Task<List<Article>> MovieNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Article> jobs = new List<Article>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.kinomania.ru/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='pagelist-item clear']"))
            {

                string title = item.SelectSingleNode(".//div[@class='pagelist-item-title']").InnerText.Trim();
                //string text = item.SelectSingleNode("//div[@class='pagelist-item clear']/p").InnerText.Trim();
                string details = "https://www.kinomania.ru/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("data-original", null).Trim();

                jobs.Add(new Article()
                {
                    Title = title,
                    //Text = text,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }

        public async Task<List<Article>> GetAllNews()
        {
            var allNews = await _context.Articles.ToListAsync();

            return allNews;

        }

        public async Task DeleteNews(int id)
        {
            var newsDb = await _context.NewsDb
                .FirstOrDefaultAsync(i => i.Id == id);

            var userNews = newsDb.News_Users.FirstOrDefault();

            _context.NewsDb.Remove(newsDb);
            _context.News_Users.Remove(userNews);

            await _context.SaveChangesAsync();
        }

        public async Task<List<News>> GetSingleNews(int id)
        {
            var list = new List<News>();

            var single = await _context.NewsDb.FirstOrDefaultAsync(i => i.Id == id);


            list.Add(single);
            

            return list;
        }

        public async Task<string> DataGraph()
        {
            var usernews = await _context.News_Users
                .Where(u => u.Id > 0)
                .Include(i => i.News)                
                .ToListAsync();

            var news = await _context.NewsDb
                .Where(n => n.IsLikes)
                .Include(i => i.News_Users)
                .ToListAsync();

            var allnews = usernews
                .Where(w => w.NewsId == w.News.Id)                 
                .ToList();

            var graphs = new List<Graphs>();

            foreach (var item in allnews)
            {
                var findedUser = await _userManager.FindByIdAsync(item.UserId);               

                var data = new Graphs
                {                    
                    NTitle = item.News.Title,
                    UserName = findedUser.UserName
                };

                graphs.Add(data);
            }
            Neo4 neo = new Neo4(graphs, _userManager);
            neo.Execute();           


            return null;                
        }

        public async Task<string> AddCommentary(int newsId, string commentary)
        {
            var authstate = await _authenticationState.GetAuthenticationStateAsync();
            var userAuth = authstate.User;
            var name = userAuth.Identity.Name;

            var createdComment = new Commentary
            {
                UserName = name,
                InputText = commentary,
                Created = DateTime.Now,
                NewsId = newsId
            };

            await _context.Commentaries.AddAsync(createdComment);
            await _context.SaveChangesAsync();

            return "Ok";        

        }

        public Task<List<Commentary>> GetCommentaries(int newsId)
        {

            var commentaries = _context.Commentaries.Where(n => n.NewsId == newsId).ToListAsync();
            
            return commentaries;
        }

        public async Task<List<Article>> GetParsedArticles()
        {
            var allnews = new List<Article>();

            var moviens = await MovieNewsScraper();
            var sportns = await SportNewsScraper();
            var belarusns = await BelarusNewsScraper();
            var gamesns = await GamingNewsScraper();
            var musicns = await MusicNewsScraper();
            var uzbekistans = await UzbekistanNewsScraper();

            foreach (var item in moviens)
            {
                allnews.Add(item);
            }
            foreach (var item in sportns)
            {
                allnews.Add(item);
            }
            foreach (var item in belarusns)
            {
                allnews.Add(item);
            }
            foreach (var item in gamesns)
            {
                allnews.Add(item);
            }
            foreach (var item in musicns)
            {
                allnews.Add(item);
            }
            foreach (var item in uzbekistans)
            {
                allnews.Add(item);
            }                

            return allnews;
        }
    }
}
