﻿using HtmlAgilityPack;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WeBScraper_CourseProject_.Data;

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
            var userId = user.Id.ToString();

            var usernews = new News_User
            {
                NewsId = news.Id,
                User = user,
                News = news
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

            return "successfully upload";

        }

        public async Task<List<News>> BelarusNewsScraper()
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

        public async Task<List<News>> UzbekistanNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.spot.uz/ru/news/");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='contentBox']"))
            {

                string title = item.SelectSingleNode(".//h2[@class='itemTitle']").InnerText.Trim();
                string details = "https://www.spot.uz/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = "https://www.spot.uz/" + item.SelectSingleNode(".//img").GetAttributeValue("data-src", null).Trim();

                jobs.Add(new News()
                {
                    Title = title,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }

        public async Task<List<News>> SportNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.mk.ru/sport/");
            foreach (var item in doc.DocumentNode.SelectNodes("//li[@class='article-listing__item']"))
            {

                string title = item.SelectSingleNode(".//h3[@class='listing-preview__title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
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

        public async Task<List<News>> GamingNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.playground.ru/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='post']"))
            {

                string title = item.SelectSingleNode(".//div[@class='post-title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
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

        public async Task<List<News>> MusicNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.mk.ru/culture/zd/");
            foreach (var item in doc.DocumentNode.SelectNodes("//li[@class='article-listing__item']"))
            {

                string title = item.SelectSingleNode(".//h3[@class='listing-preview__title']").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
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

        public async Task<List<News>> MovieNewsScraper()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> jobs = new List<News>();

            var web = new HtmlWeb();


            var doc = await web.LoadFromWebAsync("https://www.kinomania.ru/news");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='pagelist-item clear']"))
            {

                string title = item.SelectSingleNode(".//div[@class='pagelist-item-title']").InnerText.Trim();
                //string text = item.SelectSingleNode("//div[@class='pagelist-item clear']/p").InnerText.Trim();
                string details = "https://www.kinomania.ru/" + item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("data-original", null).Trim();

                jobs.Add(new News()
                {
                    Title = title,
                    //Text = text,
                    Details = details,
                    Img = img,

                }); 
            }

            return jobs;
        }
    }
}
