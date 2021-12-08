using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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

        public async Task<Posts> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<Posts>> GetPostsByUsernameAsync(string username)
        {
            return await _context.Posts.Where(x => x.AppUser.UserName == username).ToListAsync();
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