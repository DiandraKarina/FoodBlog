using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BlogNotValidException : ValidationException
    {
        internal BlogNotValidException() { }
        internal BlogNotValidException(string message) : base(message) { }
        internal BlogNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
