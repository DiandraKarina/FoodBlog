namespace API.DTO
{
    public class BlogsGetDto
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string ProfilePhoto { get; set; }
        public string BlogName { get; set; }
        public virtual UsersGetDto User { get; set; }
        public List<BlogPostGetDto> BlogPosts { get; set; }
        public List<BlogRatingGetDto> Ratings { get; set; }
    }
}
