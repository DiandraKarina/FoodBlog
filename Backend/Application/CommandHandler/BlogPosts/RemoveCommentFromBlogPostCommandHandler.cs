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
    public class RemoveCommentFromBlogPostCommandHandler// : IRequestHandler<RemoveCommentFromBlogPostCommand, OperationResult<Comment>>
    {
        /*
         private readonly IUnitOfWork _unitOfWork;
         private readonly OperationResult<Comment> _result;
         public RemoveCommentFromBlogPostCommandHandler(IUnitOfWork unitOfWork)
         {
             _unitOfWork = unitOfWork;
             _result = new OperationResult<Comment>();
         }
         public async Task<OperationResult<Comment>> Handle(RemoveCommentFromBlogPostCommand request, CancellationToken cancellationToken)
         {

             var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);
             if (blogpost == null)
             {
                 _result.AddError(ErrorCode.NotFound, BlogPostErrorMessage.BlogPostNotFound);
                 return _result;
             }
            //var comment = blogpost.Comments(request.CommentId);
            //await _unitOfWork.BlogPostRepository.GetById(request.CommentId);
            //blogpost.Comments(request.CommentId);

            if (comment == null)
             {
                 _result.AddError(ErrorCode.NotFound, BlogPostErrorMessage.CommentNotFound);
                 return _result;
             }
             if (comment.UserId != request.UserId)
             {
                 _result.AddError(ErrorCode.CommentRemovalNotAuthorized,
                     BlogPostErrorMessage.CommentRemovalNotAuthorized);
                 return _result;
             }

             blogpost.Comments.Remove(comment);

             await _unitOfWork.BlogPostRepository.Update(blogpost);
             await _unitOfWork.Save();

             _result.Payload = comment;
             return _result;
         }
        */
    }
}
