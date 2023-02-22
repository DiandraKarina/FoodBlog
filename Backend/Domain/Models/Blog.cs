using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Blog
    {
        private Blog() { }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string ProfilePhoto { get; set; }
        public string BlogName { get; set; }
        public virtual User User { get; set; }
        public virtual List<BlogPost> BlogPosts { get; set; }
        public List<BlogRating> Ratings { get; set; }

        public static Blog CreateBlog(int userid, string profilePhoto, string blogName)
        {
            return new Blog
            {
                UserId = userid,
                ProfilePhoto = profilePhoto,
                BlogName = blogName,
            };
        }
        public void AddBlogPost(BlogPost blogPost)
        {
            BlogPosts.Add(blogPost);
        }
        public void RemoveBlogPost(BlogPost blogPost)
        {
            BlogPosts.Remove(blogPost);
        }
        public void AddRating(BlogRating ratings)
        {
            Ratings.Add(ratings);
        }
        public double AverageRating(List<BlogRating> blogRatings)
        {
            double avg = blogRatings.Average(b => b.Stars);
            return avg;
        }
    }
}
