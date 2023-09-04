using System.ComponentModel.DataAnnotations;

namespace BlogPlatformWebApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEdited { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public string? Topic { get; set; }

        public int ReadTimeInMinutes { get; set; }
    }
}
