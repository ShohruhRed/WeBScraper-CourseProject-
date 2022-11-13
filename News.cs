namespace WeBScraper_CourseProject_
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public bool IsLikes { get; set; } = false;

        public List<News_User> News_Users { get; set; }
    }
}
