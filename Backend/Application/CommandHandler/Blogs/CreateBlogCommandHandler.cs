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
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, OperationResult<Blog>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBlogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Blog>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Blog>();
            try
            {
                var blog = Blog.CreateBlog(request.UserId, request.ProfilePhoto, request.BlogName);

                await _unitOfWork.BlogRepository.Add(blog);
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
