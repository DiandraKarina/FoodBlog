using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<OperationResult<Category>>
    {
        public string Name { get; set; }
        public int BlogPostId { get; set; }

    }
}
