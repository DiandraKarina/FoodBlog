using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CategoryPutPostDto
    {
        [Required]
        public string Name { get; set; }
        public int BlogPostId { get; set; }
    }
}
