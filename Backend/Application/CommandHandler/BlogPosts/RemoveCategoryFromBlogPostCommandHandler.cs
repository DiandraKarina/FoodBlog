using Application.Abstract;
using Application.Commands.BlogPosts;
using Application.Commands.Blogs;
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

namespace Application.CommandHandler.BlogPosts
{
    public class RemoveCategoryFromBlogPostCommandHandler : IRequestHandler<RemoveCategoryFromBlogPostCommand, OperationResult<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OperationResult<Category> _result;
        public RemoveCategoryFromBlogPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _result = new OperationResult<Category>();
        }
        public async Task<OperationResult<Category>> Handle(RemoveCategoryFromBlogPostCommand request, CancellationToken cancellationToken)
        {

            var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);
            if (blogpost == null)
            {
                _result.AddError(ErrorCode.NotFound, BlogPostErrorMessage.BlogPostNotFound);
                return _result;
            }
            var category = await _unitOfWork.CategoryRepository.GetById(request.CategoryId);
           

            if (category == null)
            {
                _result.AddError(ErrorCode.NotFound, CategoryErrorMessage.CategoryNotFound);
                return _result;
            }

            blogpost.Categories.Remove(category);

            await _unitOfWork.BlogPostRepository.Update(blogpost);
            await _unitOfWork.Save();

            _result.Payload = category;
            return _result;
        }

    }


}

