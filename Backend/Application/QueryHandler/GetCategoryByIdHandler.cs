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
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoriesByIdQuery, OperationResult<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Category>> Handle(GetCategoriesByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Category>();
            var category = await _unitOfWork.CategoryRepository.GetById(request.CategoryId);

            if (category is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(CategoryErrorMessage.CategoryNotFound, request.CategoryId));
                return result;
            }

            result.Payload = category;
            return result;

        }
    }


}
