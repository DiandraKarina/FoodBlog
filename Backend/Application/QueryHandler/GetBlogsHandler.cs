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
    public class GetBlogsHandler : IRequestHandler<GetBlogsQuery, OperationResult<List<Blog>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBlogsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<List<Blog>>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Blog>>();
            try
            {
                var blogs = await _unitOfWork.BlogRepository.GetAll();
                result.Payload = blogs;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
