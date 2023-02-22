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
    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, OperationResult<BlogPost>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<BlogPost>> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BlogPost>();
            try
            {
                var blog = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);

                if (blog is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(BlogPostErrorMessage.BlogPostNotFound, request.BlogPostId));
                    return result;
                }

               // if (blog.UserId != request.UserId)
               // {
                //    result.AddError(ErrorCode.BlogPostDeleteNotPossible, BlogPostErrorMessage.BlogPostDeleteNotPossible);
                //    return result;
               // }

                _unitOfWork.BlogPostRepository.Remove(blog);
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
