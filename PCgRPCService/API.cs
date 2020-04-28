using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PCModel;

namespace PCgRPCService
{
    public class API : PCgRPCService.PostCommentService.PostCommentServiceBase
    {
        private readonly ILogger<API> _logger;
        public API(ILogger<API> logger)
        {
            _logger = logger;
        }
        public override Task AddPost(Post post, ServerCallContext context)
        {
            using (var ctx = new PCModelEntities())
            {
                ctx.Posts.Add(post);
                return Task.CompletedTask;
            }
        }

        public override Task<Post> UpdatePost(Post post, ServerCallContext context)
        {
            using (var ctx = new PCModelEntities())
            {
                ctx.ChangeTracker.DetectChanges();
                var postToBeUpdated = ctx.Posts.Find(post);
                postToBeUpdated = post;
                return Task.FromResult(postToBeUpdated);
            }
        }

        public override Task DeletePost(Post post, ServerCallContext context)
        {
            using (var ctx = new PCModelEntities())
            {
                ctx.Posts.Remove(post);
                return Task.CompletedTask;
            }
        }

        public override Task<Post> GetPostById(int id, ServerCallContext context)
        {
            using (var ctx = new PCModelEntities())
            {
                return Task.FromResult(ctx.Posts.Find(id));
            }
        }
        public override Task<List<Post>> GetAllPosts()
        {
            using (var ctx = new PCModelEntities())
            {
                return Task.FromResult(ctx.Posts.ToList());
            }
        }

        public override Task AddComment(Comment comment, ServerCallContext context)
        {
            using (var ctx = new PCModelEntities())
            {
                ctx.Comments.Add(comment);
                return Task.CompletedTask;
            }
        }

        public override Task<Comment> UpdateComment(Comment comment)
        {
            using (var ctx = new PCModelEntities())
            {
                ctx.ChangeTracker.DetectChanges();
                var commentToBeUpdated = ctx.Comments.Find(comment);
                commentToBeUpdated = comment;
                return Task.FromResult(commentToBeUpdated);
            }
        }

        public override Task<Comment> GetCommentById(int id)
        {
            using (var ctx = new PCModelEntities())
            {
                return Task.FromResult(ctx.Comments.Find(id));
            }
        }
    }
}
