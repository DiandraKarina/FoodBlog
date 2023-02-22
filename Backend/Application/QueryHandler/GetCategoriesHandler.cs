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
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, OperationResult<List<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Category>>();
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAll();
                result.Payload = categories;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}

    
