using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BlogRating
    {
        private BlogRating() { }
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int Stars { get; set; }

        public static BlogRating CreateBlogRating(Blog blog, int stars, int userId)
        {
            return new BlogRating
            {
                Blog = blog,
                UserId = userId,
                Stars = stars,
            };
        }
    }
}
