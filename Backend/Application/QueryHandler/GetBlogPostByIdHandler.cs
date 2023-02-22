using Application.Abstract;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Application.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandler
{
    public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdQuery, OperationResult<BlogPost>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBlogPostByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<BlogPost>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BlogPost>();
            var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);

            if (blogpost is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BlogPostErrorMessage.BlogPostNotFound, request.BlogPostId));
                return result;
            }

            result.Payload = blogpost;
            return result;



        }
    }
}
