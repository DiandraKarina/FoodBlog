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
    public class UpdateBlogCommand : IRequest<OperationResult<Blog>>
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
       // public virtual User User { get; set; }
        public string ProfilePhoto { get; set; }
        public string BlogName { get; set; }

    }
}
