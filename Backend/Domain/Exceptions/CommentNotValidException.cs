using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CommentNotValidException : ValidationException
    {
        internal CommentNotValidException() { }
        internal CommentNotValidException(string message) : base(message) { }
        internal CommentNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
