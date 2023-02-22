using Domain.Models;
using AutoMapper;
using Application.Commands.BlogPosts;
using API.DTO;

namespace API.Profiles
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostGetDto>().ReverseMap();

            CreateMap<BlogPostPutPostDto, CreateBlogPostCommand>().ReverseMap();

            CreateMap<BlogPostPutPostDto, UpdateBlogPostCommand>();

            CreateMap<Comment, CommentsDto>().ReverseMap();

            CreateMap<PostRating, PostRatingsDto>().ReverseMap();

           

        }
    }
}
