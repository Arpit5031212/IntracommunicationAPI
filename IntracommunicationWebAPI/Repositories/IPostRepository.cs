using IntraCommunicationWebApi.Model;
using IntraCommunicationWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IntraCommunicationWebApi.Repositories
{
    public interface IPostRepository
    {
        Task<Comment> PostComment(CommentViewModel comment);
        Task<List<Comment>> GetAllComments(int postId);
        Task<Boolean> DeleteComment(int id);
        Task<List<Post>> GetPosts(int groupId);
        Task<Boolean> DeletePost(int id);
        Task<Post> AddPost(PostCreateModel post);
        Task<Boolean> LikePost(LikeViewModel like);
        Task<List<Like>> GetAllLikes(int postId);

    }
}
