using API.DTOs;

namespace API.Interfaces
{
    public interface ICommentsRepository
    {
        Task<bool> SaveAllAsync();
        Task<CommentsDto> GetCommentByIdAsync(int id);
        Task<bool> LikeCheckOnCommentAsync(LikedCommentsDto likedCommentsDto);
    }
}