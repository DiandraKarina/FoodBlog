using Application.Abstract;
using Application.Commands.Blogs;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Domain.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandler.Blogs
{
    public class UpdateBlogCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBlogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Blog>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
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
                    result.AddError(ErrorCode.BlogUpdateNotPossible, BlogsErrorMessage.BlogUpdateNotPossible);
                    return result;
                }

                blog.ProfilePhoto=request.ProfilePhoto;
                blog.BlogName = request.BlogName;

                await _unitOfWork.Save();

                result.Payload = blog;
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
