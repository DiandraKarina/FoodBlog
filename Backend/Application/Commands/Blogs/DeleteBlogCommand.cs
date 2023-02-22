using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Blogs
{
    public class DeleteBlogCommand : IRequest<OperationResult<Blog>>
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
    }
}
