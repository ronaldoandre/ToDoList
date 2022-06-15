using AutoMapper;
using Domain.Dtos.Users;
using Domain.ViewModels;
using TODOLIST.Domain.Models;
using TODOLIST.Domain.ViewModels;

namespace CrossCutting.Mappings
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap();
            CreateMap<User, UserLoginDto>()
                .ReverseMap();
            CreateMap<User, UserRegisterDto>()
                 .ReverseMap();
            CreateMap<UserViewModel, UserRegisterDto>()
                  .ReverseMap();
        }
    }
}
