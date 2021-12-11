namespace API.Entities
{
    public class LikedComments
    {
        public int Id { get; set; }
        public int CommentsId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
    }
}