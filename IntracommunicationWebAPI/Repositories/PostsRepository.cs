using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public class PostsRepository : IPostRepository
    {
        private readonly InterCommunicationDBContext dbContext;
        
        public PostsRepository(InterCommunicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Post> AddPost(PostCreateModel post)
        {
            var new_post = new Post()
            {
                PostType = "t",
                PostedOnGroup = post.PostedOnGroup,
                PostedAt = System.DateTime.Now,
                PostedBy = post.PostedBy,
                PostDescription = post.PostDescription,
                Url = "",
                StartTime = null,
                EndTime = null,
            };
            await dbContext.Posts.AddAsync(new_post);
            await dbContext.SaveChangesAsync();
            return new_post;
        }
        public async Task<List<Post>> GetPosts(int groupId)
        {
            var posts = await dbContext.Posts.Where(p => p.PostedOnGroup == groupId).Include(p => p.Likes).ToListAsync();
            return posts;
        }

        public async Task<Boolean> DeletePost(int id)
        {
            var post = await dbContext.Posts.FindAsync(id);
            if(post != null)
            {
                dbContext.Posts.Remove(post);
                dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Boolean> DeleteComment(int id)
        {
            var comment = await dbContext.Comments.FindAsync(id);
            if(comment != null)
            {
                dbContext.Comments.Remove(comment);
                dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Comment> PostComment(CommentViewModel comment)
        {
            if (comment == null) return null;

            var new_comment = new Comment()
            {
                CommentedBy = comment.CommentedBy,
                PostId = comment.PostId,
                CommentDescription = comment.CommentDescription,
                CommentedAt = System.DateTime.Now,
            };

            await dbContext.Comments.AddAsync(new_comment);
            await dbContext.SaveChangesAsync();
            return new_comment;
        }

        public async Task<List<Comment>> GetAllComments(int postId)
        {
            var post = await dbContext.Posts.FindAsync(postId);
            if(post != null )
            {
                var comments = await dbContext.Comments.Where(p => p.PostId == postId).ToListAsync();
                return comments;
            }

            return null;
        }

        public async Task<Boolean> LikePost(LikeViewModel like)
        {
            var post = await dbContext.Posts.FindAsync(like.postId);
            if(post != null)
            {
                var new_like = new Like()
                {
                    PostId = post.PostId,
                    UserId = like.userId
                };

                var isLiked = await dbContext.Likes.Where(l => l.PostId == like.postId && l.UserId == like.userId).FirstOrDefaultAsync();
                if(isLiked != null )
                {
                    dbContext.Likes.Remove(isLiked);
                    await dbContext.SaveChangesAsync();
                    return false;
                }else
                {
                    await dbContext.Likes.AddAsync(new_like);
                    await dbContext.SaveChangesAsync();
                    return true;
                }

            }

            return false;
        }

        public async Task<List<Like>> GetAllLikes(int postId)
        {
            var likes = await dbContext.Likes.Where(l => l.PostId == postId).ToListAsync();
            return likes;
        }

    }
}
