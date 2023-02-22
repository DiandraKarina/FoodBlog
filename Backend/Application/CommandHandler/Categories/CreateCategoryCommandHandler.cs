using Application.Abstract;
using Application.Commands.Blogs;
using Application.Commands.Categories;
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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OperationResult<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Category>();
            try
            {
                var category = Category.CreateCategory(request.Name,request.BlogPostId);

                await _unitOfWork.CategoryRepository.Add(category);
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
