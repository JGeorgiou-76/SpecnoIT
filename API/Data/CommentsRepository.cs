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
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CommentsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentsDto> GetCommentByIdAsync(int id)
        {
            return await _context.Comments
                .ProjectTo<CommentsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> LikeCheckOnCommentAsync(LikedCommentsDto likedCommentsDto)
        {
            var check = await _context.LikedComments
                .Where(x => x.CommentsId == likedCommentsDto.CommentsId && x.UserId == likedCommentsDto.UserId)
                .FirstOrDefaultAsync();

            if(check != null)
                return true;
            
            return false;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}