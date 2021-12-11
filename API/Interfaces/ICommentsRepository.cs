using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICommentsRepository
    {
        Task<bool> SaveAllAsync();
        Task<CommentsDto> GetCommentByIdAsync(int id);
        Task<bool> LikeCheckOnCommentAsync(LikedCommentsDto likedCommentsDto);
    }
}