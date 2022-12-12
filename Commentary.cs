using System.ComponentModel.DataAnnotations;

namespace WeBScraper_CourseProject_
{
    public class Commentary
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string InputText { get; set; }
        public DateTime Created { get; set; }
        public int NewsId { get; set; }

    }
}
