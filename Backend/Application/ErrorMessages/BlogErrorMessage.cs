using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ErrorMessages
{
    public class BlogsErrorMessage
    {
        public const string BlogNotFound = "No blog found with ID {0}";

        public const string BlogDeleteNotPossible = "Only the owner of a blog can delete it";

        public const string BlogUpdateNotPossible = "Blog update not possible";
        public const string BlogPostRemovalNotAuthorized = "Cannot remove post from blog as you are not its author";

    }
}
