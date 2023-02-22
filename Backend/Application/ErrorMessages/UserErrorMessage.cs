using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ErrorMessages
{
    public class UserErrorMessage
    {
        public const string UserNotFound = "No user found with ID {0}";

        public const string UserAlreadyExists = "An account with this email already exists";

        public const string UserUpdateNotPossible = "User update not possible";

        public const string UnauthorizedAccountRemoval = "Cannot remove this account";

    }
}
