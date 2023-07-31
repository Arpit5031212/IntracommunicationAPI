using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.Repositories;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpPost("comment")]
        public async Task<IActionResult> PostComment([FromBody] CommentViewModel comment)
        {
            if (comment != null)
            {
                var new_comment = await postRepository.PostComment(comment);
                return Ok(new_comment);
            }

            return BadRequest();
        }

        [HttpGet("comments/all")]
        public async Task<IActionResult> GetAllComments([FromQuery] int postId)
        {
            var all_comments = await postRepository.GetAllComments(postId);
            return Ok(all_comments);
           
        }

        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment_deleted = await postRepository.DeleteComment(id);
            if (comment_deleted)
            {
                return Ok(new { message = "comment deleted" });
            }
            return BadRequest();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody] PostCreateModel post)
        {
            if (post != null)
            {
                var new_post = await postRepository.AddPost(post);
                return Ok(new_post);
            }
            return BadRequest();
        }
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetPosts([FromRoute] int groupId)
        {
            var all_posts = await postRepository.GetPosts(groupId);
            if(all_posts.Count > 0)
            {
                return Ok(all_posts);
            }
            return Ok("No posts in group.");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            var post_deleted = await postRepository.DeletePost(id);
            if (post_deleted) return Ok("post deleted");
            return BadRequest();
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikePost([FromBody] LikeViewModel like)
        {
            var liked = await postRepository.LikePost(like);
            if(liked)
            {
                return Ok(new { message = "post liked" });
            }
            return Ok(new { message = "like removed" });
        }
    }
}
