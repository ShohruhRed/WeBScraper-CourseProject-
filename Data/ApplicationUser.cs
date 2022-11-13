using Microsoft.AspNetCore.Identity;

namespace WeBScraper_CourseProject_.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<News_User> News_Users { get; set; }
    }
}
