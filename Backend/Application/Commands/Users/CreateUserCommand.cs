using Application.Models;
using Domain.Models;
using Domain.Models.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class CreateUserCommand : IRequest<OperationResult<User>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
    }
}
