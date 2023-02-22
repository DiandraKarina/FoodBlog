using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PostRating
    {
        private PostRating() { }
        public int Id { get; set; }
        public BlogPost BlogPost { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int Stars { get; set; }

        public static PostRating CreatePostRating(BlogPost blogPost, int stars, int userId)
        {
            return new PostRating
            {
                BlogPost = blogPost,
                UserId = userId,
                Stars = stars,
            };
        }
    }
}
