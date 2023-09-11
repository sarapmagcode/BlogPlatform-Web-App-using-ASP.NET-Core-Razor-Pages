
namespace BlogPlatformWebApp.Models
{
    public class PostLike
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? UserId { get; set; }
    }
}
