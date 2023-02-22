using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class BlogUpdateDto
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string ProfilePhoto { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string BlogName { get; set; }
    }
}
