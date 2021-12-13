using API.Entities;

namespace API.DTOs
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int PostsId { get; set; }
        public int AppUserId { get; set; }
        public ICollection<LikedComments> LikedComments { get; set; } 
    }
}