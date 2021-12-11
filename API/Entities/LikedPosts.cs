namespace API.Entities
{
    public class LikedPosts
    {
        public int Id { get; set; }
        public int PostsId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
    }
}