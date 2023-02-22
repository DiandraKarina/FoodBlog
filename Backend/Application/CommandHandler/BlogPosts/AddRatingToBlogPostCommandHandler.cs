using Application.Abstract;
using Application.Commands.BlogPosts;
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

namespace Application.CommandHandler.BlogPosts
{
    public class AddRatingToBlogPostCommandHandler : IRequestHandler<AddRatingToBlogPostCommand, OperationResult<PostRating>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddRatingToBlogPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<PostRating>> Handle(AddRatingToBlogPostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostRating>();
            try
            {
                var blogpost = await _unitOfWork.BlogPostRepository.GetById(request.BlogPostId);
                var rating = PostRating.CreatePostRating(blogpost, request.Stars, request.UserId);

                if (blogpost == null || rating == null)
                {
                    return null;
                }

                blogpost.AddRating(rating);
                await _unitOfWork.BlogPostRepository.Update(blogpost);
                await _unitOfWork.Save();
                result.Payload = rating;
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
