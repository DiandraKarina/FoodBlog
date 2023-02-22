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
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, OperationResult<List<Comment>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCommentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<List<Comment>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
 
            var result=new OperationResult<List<Comment>>();
            try
            {
                var comments = await _unitOfWork.BlogPostRepository.GetCommentsByPostId(request.BlogPostId);
                result.Payload = comments;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
