namespace API.Entities
{
    public class Posts
    {
        public int Id { get; set; }
        public string Post { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}   