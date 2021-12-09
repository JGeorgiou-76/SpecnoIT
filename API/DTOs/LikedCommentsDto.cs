using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LikedCommentsDto
    {
        public int Id { get; set; }
        public int CommentsId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
    }
}