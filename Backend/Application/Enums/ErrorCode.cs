using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,

        //Validation errors 100 - 199
        ValidationError = 101,

        //Infrastructure errors  range 200-299
        IdentityCreationFailed = 202,
        DatabaseOperationException = 203,

        //Application errors  range 300 - 399
        BlogPostUpdateNotPossible = 300,
        BlogPostDeleteNotPossible = 301,
        BlogUpdateNotPossible = 302,
        BlogDeleteNotPossible = 303,
        UserAlreadyExists = 304,
        UserDoesNotExist = 305,
        UserUpdateNotPossible = 306,
        IncorrectPassword = 307,
        UnauthorizedAccountRemoval = 308,
        CommentRemovalNotAuthorized = 309,


        UnknownError = 999
    }
}
