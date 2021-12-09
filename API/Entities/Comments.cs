namespace API.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public Posts Post { get; set; } 
        public int PostId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }   
        public ICollection<LikedComments> LikedComments { get; set; } 
    }
}