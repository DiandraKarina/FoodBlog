using Application.Abstract;
using Application.Enums;
using Application.ErrorMessages;
using Application.Models;
using Application.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandler
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<User>();
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);

            if (user is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(UserErrorMessage.UserNotFound, request.UserId));
                return result;
            }

            result.Payload = user;
            return result;



        }
    }
}
