using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public abstract class ValidationException : Exception
    {
        internal ValidationException()
        {
            ValidationErrors = new List<string>();
        }

        internal ValidationException(string message) : base(message)
        {
            ValidationErrors = new List<string>();
        }

        internal ValidationException(string message, Exception inner) : base(message, inner)
        {
            ValidationErrors = new List<string>();
        }
        public List<string> ValidationErrors { get; }
    }
}
