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
    public class DeleteBlogPostCommand : IRequest<OperationResult<BlogPost>>
    {
        public int BlogPostId { get; set; }
        public int UserId { get; set; }
    }
}
