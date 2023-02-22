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
    public class DeleteCategoryCommand : IRequest<OperationResult<Category>>
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
