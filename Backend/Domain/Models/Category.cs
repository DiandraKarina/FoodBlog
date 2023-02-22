using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        private Category() { }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public int BlogPostId { get; set; }
        public string GetData()
        {
            return Name;
        }

        public static Category CreateCategory(string name, int blogPostId)
        {
            return new Category
            {
                Name = name,
                BlogPostId = blogPostId
            };
        }
    }
}
