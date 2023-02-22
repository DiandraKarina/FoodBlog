using Application.Abstract;
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
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<User>>();
            try
            {
                var users = await _unitOfWork.UserRepository.GetAll();
                result.Payload = users;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
