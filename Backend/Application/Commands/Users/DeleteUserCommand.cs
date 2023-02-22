using Application.Models;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<OperationResult<User>>
    {
        public int UserId { get; set; }
    }
}
