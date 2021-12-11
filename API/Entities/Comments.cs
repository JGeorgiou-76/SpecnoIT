using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Posts Posts { get; set; } 
        public int PostsId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }   
        public ICollection<LikedComments> LikedComments { get; set; } 
    }
}