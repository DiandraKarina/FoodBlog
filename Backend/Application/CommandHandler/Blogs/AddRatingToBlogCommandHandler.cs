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
    public class AddRatingToBlogCommandHandler : IRequestHandler<AddRatingToBlogCommand, OperationResult<BlogRating>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddRatingToBlogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<BlogRating>> Handle(AddRatingToBlogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BlogRating>();
            try
            {
                var blog = await _unitOfWork.BlogRepository.GetById(request.BlogId);
                var rating = BlogRating.CreateBlogRating(blog, request.Stars, request.UserId);

                if (blog == null || rating == null)
                {
                    return null;
                }

                blog.AddRating(rating);
                await _unitOfWork.BlogRepository.Update(blog);
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
