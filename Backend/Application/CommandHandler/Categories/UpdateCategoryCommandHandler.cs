using Application.Abstract;
using Application.Commands.Categories;
using Application.Commands.Users;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandler.Categories
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<OperationResult<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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

                category.Name = request.Name;


                await _unitOfWork.Save();

                result.Payload = category;
            }

            catch (BlogPostNotValidException e)
            {
                e.ValidationErrors.ForEach(er => result.AddError(ErrorCode.ValidationError, er));
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }

    }
}
