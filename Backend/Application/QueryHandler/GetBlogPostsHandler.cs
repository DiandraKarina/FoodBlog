using Application.Abstract;
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
    public class GetBlogPostsHandler : IRequestHandler<GetBlogPostsQuery, OperationResult<List<BlogPost>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBlogPostsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<List<BlogPost>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<BlogPost>>();
            try
            {
                var blogposts = await _unitOfWork.BlogPostRepository.GetAll();
                result.Payload = blogposts;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
