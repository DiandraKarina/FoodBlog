using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class BlogPostPutPostDto
    {
        [Required]
        public int BlogId { get; set; }
       // [Required]
       // public int CategoryId { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string PostName { get; set; }

        [Required]
        [MaxLength(400)]
        [MinLength(20)]
        public string Description { get; set; }
       
    }
}
