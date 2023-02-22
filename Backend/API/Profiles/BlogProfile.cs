using API.DTO;
using Application.Commands.Blogs;
using AutoMapper;
using Domain.Models;

namespace API.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogsGetDto>().ReverseMap();

            CreateMap<BlogPostDto, CreateBlogCommand>().ReverseMap();

            CreateMap<BlogUpdateDto, UpdateBlogCommand>();

            CreateMap<BlogPost, BlogPostGetDto>().ReverseMap();

            CreateMap<BlogRating, BlogRatingGetDto>().ReverseMap();
        }
    }
}
