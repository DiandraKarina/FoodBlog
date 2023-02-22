using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BlogPostNotValidException : ValidationException
    {
        internal BlogPostNotValidException() { }
        internal BlogPostNotValidException(string message) : base(message) { }
        internal BlogPostNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
