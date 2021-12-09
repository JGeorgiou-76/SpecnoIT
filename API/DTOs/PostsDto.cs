using API.Entities;

namespace API.DTOs
{
    public class PostsDto
    {
        public int Id { get; set; }
        public string Post { get; set; }
        public int AppUserId { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}