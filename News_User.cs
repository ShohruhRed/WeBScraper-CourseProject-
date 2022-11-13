using WeBScraper_CourseProject_.Data;

namespace WeBScraper_CourseProject_
{
    public class News_User
    {
        public int Id { get; set; }

        public int NewsId { get; set; }
        public News News { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}