using AutoMapper;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.Roles;
using LemmeProject.Application.DTOs.Users;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Entities.Identity;

namespace LemmeProject.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser,UserAddRequest>().ReverseMap();
            CreateMap<AppUser,UserUpdateRequest>().ReverseMap();
            CreateMap<AppUser,UserToUpdateResponse>().ReverseMap();
            CreateMap<AppUser,UserTableResponse>().ReverseMap();
            CreateMap<AppUser,UserResetPasswordRequest>().ReverseMap();
            CreateMap<AppUser,UserChangePasswordRequest>().ReverseMap();
            CreateMap<AppUser,LoginRequest>().ReverseMap();
            CreateMap<AppUser,LoginedUserResponse>().ReverseMap();


            CreateMap<AppRole,RoleAddRequest>().ReverseMap();
            CreateMap<AppRole,RoleTableResponse>().ReverseMap();

            CreateMap<Product, ProductAddRequest>().ReverseMap();
            CreateMap<Product, ProductUpdateRequest>().ReverseMap();
            CreateMap<Product, ProductTableResponse>().ReverseMap();


            CreateMap<Image, ImageAddRequest>().ReverseMap();


        }
    }
}
