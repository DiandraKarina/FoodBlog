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
    public class CreateBlogPostCommand : IRequest<OperationResult<BlogPost>>
    {
       // public Blog Blog { get; set; }
       public int BlogId { get; set; }
      //  public int CategoryId { get; set; }
        public string Image { get; set; }
        public string PostName { get; set; }
        public string Description { get; set; }
       // public Category Category { get; set; }
    }
}
