using Application.Abstract;
using Application.Commands.Users;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandler.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public async Task<OperationResult<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<User>();

            try
            {
                var user = await _unitOfWork.UserRepository.GetById(request.UserId);
               

                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(UserErrorMessage.UserNotFound, request.UserId));
                    return result;
                }

                if (user.UserId != request.UserId)
                {
                    result.AddError(ErrorCode.UserUpdateNotPossible, UserErrorMessage.UserUpdateNotPossible);
                    return result;
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Gender = request.Gender;
                user.EmailAddress = request.EmailAddress;

                await _unitOfWork.Save();

                result.Payload = user;
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
