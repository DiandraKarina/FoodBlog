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
    public class AddRatingToBlogCommand : IRequest<OperationResult<BlogRating>>
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
    }
}
