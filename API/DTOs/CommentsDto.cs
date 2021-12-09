using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int AppUserId { get; set; }
    }
}