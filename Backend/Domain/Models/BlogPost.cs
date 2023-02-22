using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class BlogPost
    {
        private BlogPost() { }
        public string Image { get; set; }
        public string PostName { get; set; }
        public string Description { get; set; }
        public int BlogPostId { get; set; }
        public virtual Blog Blog { get; set; }
        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PostRating> PostRatings { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Category> Categories { get; set; }

        //Factory                               ???
        public static BlogPost CreateBlogpost(int blogId,/* int categoryId,*/string image, string postName, string description)
        {
            return new BlogPost
            {
                BlogId=blogId,
               /* CategoryId=categoryId,*/
                Image = image,
                PostName = postName,
                Description=description,
                CreatedDate = DateTime.Now,
            };
        }
        public void AddComment(Comment newComment)
        {
            Comments.Add(newComment);
            CreatedDate = DateTime.Now;
        }
        public void RemoveComment(Comment delComment)
        {
            Comments.Remove(delComment);
        }

        public void AddCategory(Category newCategory)
        {
            Categories.Add(newCategory);
            
        }
        public void RemoveCategory(Category delCategory)
        {
            Categories.Remove(delCategory);
        }

        public void AddRating(PostRating postRating)
        {

            PostRatings.Add(postRating);
        }

        public double AverageRating(List<PostRating> postRatings)
        {
            double avg = postRatings.Average(b => b.Stars);
            return avg;
        }
    }
}
