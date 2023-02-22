using Application.Abstract;
using Application.Commands.BlogPosts;
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

namespace Application.CommandHandler.BlogPosts
{
    public class AddCommentToBlogPostCommandHandler : IRequestHandler<AddCommentToBlogPostCommand, OperationResult<Comment>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddCommentToBlogPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<Comment>> Handle(AddCommentToBlogPostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Comment>();
            try
            {
                var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);
                var comment = Comment.CreateComment(request.BlogPostId, request.Message);//, request.UserId);
                if (blogpost is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(BlogPostErrorMessage.BlogPostNotFound, request.BlogPostId));
                    return result;
                }

                blogpost.AddComment(comment);

                await _unitOfWork.BlogPostRepository.Update(blogpost);
                await _unitOfWork.Save();

                result.Payload = comment;

            }

            catch (CommentNotValidException e)
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
