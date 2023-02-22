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
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, OperationResult<BlogPost>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBlogPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<BlogPost>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BlogPost>();
            try
            {
                var blogpost = BlogPost.CreateBlogpost(request.BlogId, request.Image, request.PostName, request.Description);

                await _unitOfWork.BlogPostRepository.Add(blogpost);
                await _unitOfWork.Save();
                result.Payload = blogpost;
            }
            catch (BlogPostNotValidException e)
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
