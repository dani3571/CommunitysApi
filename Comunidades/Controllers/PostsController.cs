using Comunidades.Models;
using Comunidades.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comunidades.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _postService.CreatePost(post);
            return Ok();
        }
        //obtener los comentarios de un post
        [HttpGet("{postId}/comments")]
        public IActionResult GetCommentsByPostId(string postId)
        {
            var comments = _postService.GetCommentsByPostId(postId);
            return Ok(comments);
        }
        [HttpGet("{postId}/reactions")]
        public IActionResult GetReactionsByPostId(string postId)
        {
            var reactions = _postService.GetReactionsByPostId(postId);
            return Ok(reactions);
        }
    }


}
