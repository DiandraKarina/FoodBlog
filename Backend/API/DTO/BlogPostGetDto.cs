using Domain.Models;

namespace API.DTO
{
    public class BlogPostGetDto
    {
        public int BlogPostId { get; set; }
        public string Image { get; set; }
        public string PostName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryGetDto Category { get; set; }
        public List<CommentsDto> Comments { get; set; }
        public List<PostRatingsDto> Ratings { get; set; }
    }
}
