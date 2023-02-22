using Application.Abstract;
using Application.Commands.Blogs;
using Application.Enums;
using Application.Models;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandler.Blogs
{
    public class AddBlogPostToBlogHandler : IRequestHandler<AddBlogPostToBlogCommand, OperationResult<Blog>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBlogPostToBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Blog>> Handle(AddBlogPostToBlogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Blog>();
            try
            {
                var blog = await _unitOfWork.BlogRepository.GetById(request.BlogId);
                var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);

                if (blog == null || blogpost == null)
                {
                    return null;
                }

                blog.BlogPosts.Add(blogpost);
                await _unitOfWork.Save();
            }
            catch (BlogNotValidException e)
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
