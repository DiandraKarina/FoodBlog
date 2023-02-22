using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCommentsQuery : IRequest<OperationResult<List<Comment>>>
    {
        public int BlogPostId { get; set; }
    }

}
