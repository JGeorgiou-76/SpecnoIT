using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Comments> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        // public int Countlikes(int postId)
        // {
        //     return _context.LikedPosts.Where(x => x.PostId == postId && x.Liked).Count();
        // }

        public async Task<Posts> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<PostsDto>> GetPostsByUsernameAsync(string username)
        {
            return await _context.Posts
                .Include(x => x.Comments)
                .Where(x => x.AppUser.UserName == username)
                .ProjectTo<PostsDto>(_mapper.ConfigurationProvider)             
                .ToListAsync();
        }

        public async Task<IEnumerable<LikedPostsDto>> GetPostsUserHasLikedAsync(int userId)
        {
            return await _context.LikedPosts
                .Where(x => x.UserId == userId)
                .ProjectTo<LikedPostsDto>(_mapper.ConfigurationProvider)                
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdatePost(Posts posts)
        {
            _context.Entry(posts).State = EntityState.Modified;
        }
    }
}