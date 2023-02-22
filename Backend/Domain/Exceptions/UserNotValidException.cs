using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotValidException : ValidationException
    {
        internal UserNotValidException() { }
        internal UserNotValidException(string message) : base(message) { }
        internal UserNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
