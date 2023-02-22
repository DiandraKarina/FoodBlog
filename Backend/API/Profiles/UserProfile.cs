using API.DTO;
using Application.Commands.Users;
using AutoMapper;
using Domain.Models;

namespace API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UsersGetDto>().ReverseMap();

            CreateMap<UserPostDto, CreateUserCommand>();

            CreateMap<UserUpdateDto, UpdateUserCommand>();

            CreateMap<Blog, BlogsGetDto>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
