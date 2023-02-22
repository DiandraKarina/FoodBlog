using API.DTO;
using Application.Commands.BlogPosts;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net.Http.Headers;

namespace API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
   
    public class BlogPostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BlogPostController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // GET: api/<BlogPostsController>
        [HttpGet]
        public async Task<IActionResult> GetBlogPosts(CancellationToken cancellationToken)
        {
            var query = new GetBlogPostsQuery();
            var response = await _mediator.Send(query, cancellationToken);
            var blogposts = _mapper.Map<List<BlogPostGetDto>>(response.Payload);

            return Ok(blogposts);
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostsById(int id, CancellationToken cancellationToken)
        {
            var query = new GetBlogPostByIdQuery
            {
                BlogPostId = id
            };

            var response = await _mediator.Send(query, cancellationToken);

            var blogpost = _mapper.Map<BlogPostGetDto>(response.Payload);

            return Ok(blogpost);
        }

        // POST api/<BlogPostsController>
        [HttpPost("create-blogpost")]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostPutPostDto blogpost, CancellationToken cancellationToken)//UserCreateUpdate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreateBlogPostCommand>(blogpost);
            var response = await _mediator.Send(command, cancellationToken);
            var dto = _mapper.Map<BlogPostGetDto>(response.Payload);

            return 
                CreatedAtAction(nameof(GetBlogPostsById), new { id = response.Payload.BlogPostId }, dto);
        }

        // PUT api/<BlogPostsController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPostPutPostDto updatedBlogPost, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateBlogPostCommand>(updatedBlogPost);
            command.BlogPostId = id;
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            return  NoContent();                     //?
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteBlogPostCommand() { BlogPostId = id };
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            return NoContent();
        }


        [HttpPost("{blogPostId}/comments")]
        public async Task<IActionResult> AddCommentToBlogPost(int blogPostId,[FromBody] string message, CancellationToken cancellationToken)
        {

            var command = new AddCommentToBlogPostCommand()
            {
                BlogPostId = blogPostId,
                Message=message
              //  CommentId = commentId                    
            };

            var response = await _mediator.Send(command, cancellationToken);


            var newComment = _mapper.Map<CommentsDto>(response.Payload);

            return Ok(newComment);
        }

        [HttpGet("{blogPostId}/comments")]
        public async Task<IActionResult> GetComments(CancellationToken cancellationToken)
        {
            var query = new GetCommentsQuery();
            var response = await _mediator.Send(query, cancellationToken);
            var comments = _mapper.Map<List<CommentsDto>>(response.Payload);

            return Ok(comments);
        }


        [HttpPost("{blogPostId}/{categoryId}")]
        public async Task<IActionResult> AddCategoryToBlogPost(int blogPostId, int categoryId, CancellationToken cancellationToken)
        {

            var command = new AddCategoryToBlogPostCommand()
            {
                BlogPostId = blogPostId,
                CategoryId = categoryId                    
            };

            var response = await _mediator.Send(command, cancellationToken);


            var newCategory = _mapper.Map<CommentsDto>(response.Payload);

            return Ok(newCategory);
        }


        [HttpDelete]
        [Route("{blogPostId}/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryFromBlogPost(int blogPostId, int categoryId, CancellationToken cancellationToken)
        {
            var command = new RemoveCategoryFromBlogPostCommand
            {
                BlogPostId = blogPostId,
                CategoryId = categoryId
            };
            var response = await _mediator.Send(command);

            return Ok(_mapper.Map<CategoryGetDto>(response.Payload));


        }

        [HttpDelete]
        [Route("{blogPostId}/comments/{commentId}")]
        public async Task<IActionResult> RemoveCommentFromBlogPost(int blogPostId, int commentId, CancellationToken cancellationToken)
        {
            var command = new RemoveCommentFromBlogPostCommand
            {
                BlogPostId = blogPostId,
                CommentId = commentId
            };
            var response = await _mediator.Send(command);

            return Ok(_mapper.Map<CommentsDto>(response.Payload));


        }

        /*
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = "Image";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    //newImageName = fileName;
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        */
        

    }
}
