using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostsRepository
    {
        void UpdatePost(Posts posts);
        Task<bool> SaveAllAsync();
        Task<bool> LikeCheckOnPostAsync(LikedPostsDto likedPostsDto);
        Task<PostsDto> GetPostDtoByIdAsync(int id); 
        Task<Posts> GetPostByIdAsync(int id);     
        Task<IEnumerable<PostsDto>> GetPostsByUsernameAsync(string username);
        Task<List<PostsDto>> GetPostsUserHasLikedAsync(int userId);
        Task<IEnumerable<CommentsDto>> GetAllCommentsOnPostAsync(int postId); 
    }
}