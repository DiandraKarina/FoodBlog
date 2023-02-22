using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BlogPosts
{
    public class RemoveCommentFromBlogPostCommand : IRequest<OperationResult<Comment>>
    {
        public int BlogPostId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}
