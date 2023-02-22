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
    public class RemoveBlogPostFromBlogCommandHandler : IRequestHandler<RemoveBlogPostFromBlogCommand, OperationResult<BlogPost>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OperationResult<BlogPost> _result;
        public RemoveBlogPostFromBlogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _result = new OperationResult<BlogPost>();
        }
        public async Task<OperationResult<BlogPost>> Handle(RemoveBlogPostFromBlogCommand request, CancellationToken cancellationToken)
        {

            var blog = await _unitOfWork.BlogRepository.GetById(request.BlogId);
            if (blog == null)
            {
                _result.AddError(ErrorCode.NotFound, BlogsErrorMessage.BlogNotFound);
                return _result;
            }
            var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);

            if (blogpost == null)
            {
                _result.AddError(ErrorCode.NotFound, BlogPostErrorMessage.BlogPostNotFound);
                return _result;
            }
            if (blogpost.UserId != request.UserId)
            {
                _result.AddError(ErrorCode.BlogPostDeleteNotPossible,
                    BlogPostErrorMessage.BlogPostDeleteNotPossible);
                return _result;
            }

            blog.RemoveBlogPost(blogpost);

            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Save();

            _result.Payload = blogpost;
            return _result;
        }
    }
}
