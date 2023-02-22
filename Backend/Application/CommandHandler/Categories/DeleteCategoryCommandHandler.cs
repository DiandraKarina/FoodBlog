using Application.Abstract;
using Application.CommandHandler.Users;
using Application.Commands.Categories;
using Application.Commands.Users;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandler.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, OperationResult<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<OperationResult<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Category>();
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(request.CategoryId);

                if (category is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(CategoryErrorMessage.CategoryNotFound, request.CategoryId));

                    return result;
                }

                _unitOfWork.CategoryRepository.Remove(category);
                await _unitOfWork.Save();

                result.Payload = category;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }


    }
}
