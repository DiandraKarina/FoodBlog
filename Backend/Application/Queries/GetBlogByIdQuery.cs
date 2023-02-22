﻿using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetBlogByIdQuery : IRequest<OperationResult<Blog>>
    {
        public int BlogId { get; set; }
    }
}
