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
    public class GetBlogByIdHandler : IRequestHandler<GetBlogByIdQuery, OperationResult<Blog>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBlogByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Blog>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Blog>();
            var blog = await _unitOfWork.BlogRepository.GetById(request.BlogId);

            if (blog is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(BlogsErrorMessage.BlogNotFound, request.BlogId));
                return result;
            }

            result.Payload = blog;
            return result;

        }
    }
}
