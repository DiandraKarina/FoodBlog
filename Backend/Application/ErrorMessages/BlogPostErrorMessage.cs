using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ErrorMessages
{
    public class BlogPostErrorMessage
    {
        public const string BlogPostNotFound = "No blogpost found with ID {0}";

        public const string BlogPostDeleteNotPossible = "Only the owner of a blogpost can delete it";

        public const string BlogPostUpdateNotPossible = "Blogpost update not possible";

        public const string CommentNotFound = "No comment found with ID {0}";

        public const string CommentRemovalNotAuthorized = "Only the owner of a comment can delete it";

        public const string CommentUpdateNotPossible = "Comment update not possible";
    }
}
