using API.DTO;
using Application.Commands.Blogs;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;


namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BlogController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // GET: api/<BlogController>
        [HttpGet]
        public async Task<IActionResult> GetBlogs(CancellationToken cancellationToken)
        {
            var query = new GetBlogsQuery();
            var response = await _mediator.Send(query, cancellationToken);
            var blogs = _mapper.Map<List<BlogsGetDto>>(response.Payload);

            return Ok(blogs);
        }

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id, CancellationToken cancellationToken)
        {
            var query = new GetBlogByIdQuery
            {
                BlogId = id
            };

            var response = await _mediator.Send(query, cancellationToken);

            var blog = _mapper.Map<BlogsGetDto>(response.Payload);

            return  Ok(blog);
        }

        /* [HttpPost]
         public async Task<IActionResult> CreateBlog([FromBody] BlogCreateUpdate blog, CancellationToken cancellationToken)//UserCreateUpdate user)
         {
             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             var command = new CreateBlogCommand()
             {
                 ProfilePhoto=blog.ProfilePhoto,
                 BlogName=blog.BlogName,

             };
             var response = await _mediator.Send(command, cancellationToken);
             var dto = _mapper.Map<BlogResponse>(response.Payload);

             return CreatedAtAction(nameof(GetBlogById), new { blogId = response.Payload.BlogId }, dto);
         }
        
        */
        // POST api/<BlogController>
        [HttpPost("create-blog")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogPostDto blog, CancellationToken cancellationToken)//UserCreateUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreateBlogCommand>(blog);
            var response = await _mediator.Send(command, cancellationToken);
            var dto = _mapper.Map<BlogsGetDto>(response.Payload);

            return 
             CreatedAtAction(nameof(GetBlogById), new { id = response.Payload.BlogId }, dto);
        }

        // PUT api/<BlogController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogUpdateDto updatedBlog)
        {
            var command = _mapper.Map<UpdateBlogCommand>(updatedBlog);
            command.BlogId = id;
            var response = await _mediator.Send(command);

            if (response == null)
                return NotFound();

            return  NoContent();                     
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteBlogCommand() { BlogId = id };
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            return  NoContent();
        }
        [HttpPost]
        [Route("{blogId}/blogposts/{blogPostId}")]
        public async Task<IActionResult> AddBlogPostToBlog(int blogId, int blogPostId, CancellationToken cancellationToken)
        {
            var command = new AddBlogPostToBlogCommand
            {
                BlogId = blogId,
                BlogPostId = blogPostId
            };
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(_mapper.Map<BlogsGetDto>(response.Payload));
        }

        [HttpDelete]
        [Route("{blogId}/blogposts/{blogPostId}")]
        public async Task<IActionResult> RemoveBlogPostFromBlog(int blogId, int blogPostId, CancellationToken cancellationToken)
        {
            var command = new RemoveBlogPostFromBlogCommand { BlogId = blogId, BlogPostId = blogPostId };
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(_mapper.Map<BlogsGetDto>(response.Payload));

        }
        
    }

}
