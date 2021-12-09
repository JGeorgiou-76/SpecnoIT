using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Posts, PostsDto>();
            CreateMap<PostsDto, Posts>();
            CreateMap<Comments, CommentsDto>();
            CreateMap<LikedPosts, LikedPostsDto>();
            CreateMap<LikedComments, LikedCommentsDto>();
            //CreateMap<LikedPosts, PostsDto>();
        }
    }
}