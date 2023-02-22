using Application.Abstract;
using Application.Commands.Users;
using Application.Enums;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OperationResult<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<User>();
            try
            {
                var user = User.CreateUser(request.FirstName, request.LastName, request.Gender, request.EmailAddress, request.DOB);
                _logger.LogInformation($"Created account for user {user.FirstName} {user.LastName}");
                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.Save();

                result.Payload = user;
            }
            catch (UserNotValidException e)
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
