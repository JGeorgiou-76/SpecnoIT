namespace API.DTOs
{
    public class LikedPostsDto
    {
        public int Id { get; set; }
        public int PostsId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
    }
}