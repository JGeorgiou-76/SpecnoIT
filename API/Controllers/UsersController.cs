using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPostsRepository _postsRepository;
        private readonly DataContext _context;
        public UsersController(DataContext context, 
                                IUserRepository userRepository, 
                                IMapper mapper, 
                                IPostsRepository postsRepository)
        {
            _context = context;
            _postsRepository = postsRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //Get all Users in the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }
        //Get the a specific user by their Username
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUsername(string username) {
            return await _userRepository.GetMemberAsync(username);
        }

        //Allow the Logged in user to create a post
        [HttpPost("add-post")]
        public async Task<ActionResult<PostsDto>> AddPost(PostsDto postsDto) {
            //This Variable gets the username from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            if(postsDto.Post == null)
                return BadRequest("Cannot Post an Empty Post");

            var newPost = new Posts{
                Post = postsDto.Post,
                AppUserId = user.Id,
                AppUser = user
            };

            _context.Posts.Add(newPost);

            if(await _userRepository.SaveAllAsync()){
                return _mapper.Map<PostsDto>(newPost);
            }

            return BadRequest("Problem Creating New Post");
        }

        [HttpPut("update-post")]
        public async Task<ActionResult> UpdatePost(PostsDto postsDto) {
            //This Variable gets the username of the Logged in User from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            postsDto.AppUserId = user.Id;
            
            var selectedPost = user.Posts.FirstOrDefault(x => x.Id == postsDto.Id);

            if(selectedPost == null)
                return NotFound();

            //Check to make sure that the User ID matched the UserId of the Post that is being updated, A User can only update their Posts
            if(postsDto.AppUserId != selectedPost.AppUserId)
                return BadRequest("Cannot Update another Users Post");         

            _mapper.Map(postsDto, selectedPost);

            _postsRepository.UpdatePost(selectedPost);

            if(await _postsRepository.SaveAllAsync())
                return NoContent();
            
            return BadRequest("Failed to Update Post");
        }

        [HttpDelete("delete-post")]
        public async Task<ActionResult> DeletePost(PostsDto postDto){
            //This Variable gets the username of the Logged in User from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            var selectedPost = user.Posts.FirstOrDefault(x => x.Id == postDto.Id);

            if(selectedPost == null)
                return NotFound();
            
            _context.Posts.Remove(selectedPost);

            if(await _userRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Failed to Delete Post");
        }

        //Function to get all the posts of a logged in User
        [HttpGet("my-posts")]
        public async Task<IEnumerable<PostsDto>> GetUserPosts() {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _postsRepository.GetPostsByUsernameAsync(username);
        }

        //Function to get all the posts of a specific User
        [HttpGet("user-posts/{username}")]
        public async Task<IEnumerable<PostsDto>> GetPostsByUsername(string username) {

            return await _postsRepository.GetPostsByUsernameAsync(username);
        }

        //Function to Create comments on posts
        [HttpPost("create-comment")]
        public async Task<ActionResult<CommentsDto>> CreateNewComment(CommentsDto commentsDto){
            //This Variable gets the username from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            //Getting the Specific post that the comment is to be attached too by the Post ID
            var post = await _postsRepository.GetPostByIdAsync(commentsDto.PostId);

            if(post == null)
                return BadRequest("Post does not Exist");

            if(commentsDto.Comment == null)
                return BadRequest("Cannot Create an Empty Comment");

            var newComment = new Comments{
                Comment = commentsDto.Comment,
                PostId = commentsDto.PostId,
                AppUserId = user.Id,
                AppUser = user,
                Post = post
            };

            _context.Comments.Add(newComment);
        
            if(await _postsRepository.SaveAllAsync()){
                return _mapper.Map<CommentsDto>(newComment);
            }

            return BadRequest("Problem Creating New Comment");
        }

        //Function to like a Post
        [HttpPost("like-post")]
        public async Task<ActionResult<LikedPostsDto>> LikePost(LikedPostsDto likedPostsDto){
            //This Variable gets the username from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            //Getting the Specific post that will be liked or Disliked via the postID
            var post = await _postsRepository.GetPostByIdAsync(likedPostsDto.PostsId);

            if(post == null)
                return BadRequest("Post does not Exist");

            
            var newLikedPost = new LikedPosts{
                PostsId = post.Id,
                Posts = post,
                UserId = user.Id,
                Liked = likedPostsDto.Liked
            };

            _context.LikedPosts.Add(newLikedPost);

            if(await _postsRepository.SaveAllAsync()){
                return _mapper.Map<LikedPostsDto>(newLikedPost);
            }

            return BadRequest("Problem Liking post");
        }

        //Function to like a Comment
        [HttpPost("like-comment")]
        public async Task<ActionResult<LikedCommentsDto>> LikeComment(LikedCommentsDto likedCommentsDto){
            //This Variable gets the username from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            //Getting the Specific comment that will be liked or Disliked via the postID
            var comment = await _postsRepository.GetCommentByIdAsync(likedCommentsDto.CommentsId);

            if(comment == null)
                return BadRequest("Post does not Exist");

            
            var newLikedComment = new LikedComments{
                CommentsId = comment.Id,
                Comments = comment,
                UserId = user.Id,
                Liked = likedCommentsDto.Liked
            };

            _context.LikedComments.Add(newLikedComment);

            if(await _postsRepository.SaveAllAsync()){
                return _mapper.Map<LikedCommentsDto>(newLikedComment);
            }

            return BadRequest("Problem Liking Comment");
        }

        [HttpGet("liked-posts")]
        public async Task<IEnumerable<LikedPostsDto>> GetPostsUserHasLiked() {
            //This Variable gets the username from the token 
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetAppUserByUsernameAsync(username);

            return await _postsRepository.GetPostsUserHasLikedAsync(user.Id);
        }
    }   
}