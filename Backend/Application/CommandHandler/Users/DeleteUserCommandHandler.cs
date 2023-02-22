using Application.Abstract;
using Application.Commands.Users;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OperationResult<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
           // _logger = logger;
        }

        public async Task<OperationResult<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<User>();
            try
            {
                var user = await _unitOfWork.UserRepository.GetById(request.UserId);
               // _logger.LogInformation($"Deleted account for user {user.FirstName} {user.LastName}");

                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(UserErrorMessage.UserNotFound, request.UserId));

                    return result;
                }
                if (user.UserId != request.UserId)
                {
                    result.AddError(ErrorCode.NotFound,
                        string.Format(UserErrorMessage.UnauthorizedAccountRemoval, request.UserId));

                    return result;
                }

                _unitOfWork.UserRepository.Remove(user);
                await _unitOfWork.Save();

                result.Payload = user;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
