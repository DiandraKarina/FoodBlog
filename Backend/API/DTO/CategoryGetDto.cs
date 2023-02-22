using Domain.Models;

namespace API.DTO
{
    public class CategoryGetDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public BlogPostGetDto BlogPost { get; set; }
    }
}
