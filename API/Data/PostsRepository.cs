using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PostsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        public async Task<PostsDto> GetPostDtoByIdAsync(int id)
        {
            return await _context.Posts.ProjectTo<PostsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Posts> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<PostsDto>> GetPostsByUsernameAsync(string username)
        {
            return await _context.Posts
                .Include(x => x.Comments)
                .Where(x => x.AppUser.UserName == username)
                .ProjectTo<PostsDto>(_mapper.ConfigurationProvider)             
                .ToListAsync();
        }

        public async Task<IEnumerable<CommentsDto>> GetAllCommentsOnPostAsync(int postId)
        {
            return await _context.Comments
                .Where(x => x.PostsId == postId)
                .ProjectTo<CommentsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<PostsDto>> GetPostsUserHasLikedAsync(int userId)
        {
            var likedPosts = _context.LikedPosts
                .Where(x => x.UserId == userId)
                .ProjectTo<LikedPostsDto>(_mapper.ConfigurationProvider)                
                .ToList();

            var posts = new List<PostsDto> {};

            foreach(var list in likedPosts){
                var x = await GetPostDtoByIdAsync(list.PostsId);
                posts.Add(x);  
            }

            return posts;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdatePost(Posts posts)
        {
            _context.Entry(posts).State = EntityState.Modified;
        }

        public async Task<bool> LikeCheckOnPostAsync(LikedPostsDto likedPostsDto)
        {
            var check = await _context.LikedPosts
                .Where(x => x.PostsId == likedPostsDto.PostsId && x.UserId == likedPostsDto.UserId)
                .FirstOrDefaultAsync();

            if(check != null)
                return true;
            
            return false;
        }
    }
}