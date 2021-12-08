using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostsRepository
    {
        void UpdatePost(Posts posts);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Posts>> GetPostsAsync();
        Task<Posts> GetPostByIdAsync(int id);
        Task<IEnumerable<Posts>> GetPostsByUsernameAsync(string username);
    }
}