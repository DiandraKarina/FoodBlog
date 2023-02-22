using API.DTO;
using Application.Commands.Categories;
using AutoMapper;
using Domain.Models;

namespace API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>().ReverseMap();
            CreateMap<CategoryPutPostDto, CreateCategoryCommand>();
            CreateMap<CategoryPutPostDto,UpdateCategoryCommand>();
        }
    }
}
