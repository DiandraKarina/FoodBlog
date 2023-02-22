using Application.Abstract;
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

namespace Application.CommandHandler.Blogs
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, OperationResult<Blog>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Blog>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Blog>();
            try
            {
                var blog = await _unitOfWork.BlogRepository.GetById(request.BlogId);

                if (blog is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(BlogsErrorMessage.BlogNotFound, request.BlogId));
                    return result;
                }

                if (blog.UserId != request.UserId)
                {
                    result.AddError(ErrorCode.BlogDeleteNotPossible, BlogsErrorMessage.BlogDeleteNotPossible);
                    return result;
                }

                _unitOfWork.BlogRepository.Remove(blog);
                await _unitOfWork.Save();

                result.Payload = blog;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
