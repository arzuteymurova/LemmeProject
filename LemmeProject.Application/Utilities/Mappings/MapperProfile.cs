using AutoMapper;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.DTOs.Questions;
using LemmeProject.Application.DTOs.Roles;
using LemmeProject.Application.DTOs.Stores;
using LemmeProject.Application.DTOs.Users;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Entities.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace LemmeProject.Application.Utilities.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, UserAddRequest>().ReverseMap();
            CreateMap<AppUser, UserUpdateRequest>().ReverseMap();
            CreateMap<AppUser, UserToUpdateResponse>().ReverseMap();
            CreateMap<AppUser, UserTableResponse>().ReverseMap();
            CreateMap<AppUser, UserResetPasswordRequest>().ReverseMap();
            CreateMap<AppUser, UserChangePasswordRequest>().ReverseMap();
            CreateMap<AppUser, LoginRequest>().ReverseMap();
            CreateMap<AppUser, LoginedUserResponse>().ReverseMap();


            CreateMap<AppRole, RoleAddRequest>().ReverseMap();
            CreateMap<AppRole, RoleTableResponse>().ReverseMap();

            CreateMap<ProductAddRequest, Product>()
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Images = src.Images.Select(image => new ProductImage {}).ToList();
            });
            CreateMap<ProductUpdateRequest, Product>()
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Images = src.Images.Select(image => new ProductImage { }).ToList();
            });
            CreateMap<Product, ProductTableResponse>().ReverseMap();
            CreateMap<Product, ProductTableByNameResponse>().ReverseMap();


            CreateMap<ProductImage, ProductImageAddRequest>().ReverseMap();
            CreateMap<ProductImage, ProductImageTableResponse>().ReverseMap();

            CreateMap<ProductSearchHistory, ProductSearchHistoryAddRequest>().ReverseMap();
            CreateMap<ProductSearchHistory, ProductSearchHistoryTableResponse>().ReverseMap();

            CreateMap<Question, QuestionAddRequest>().ReverseMap();
            CreateMap<Question, QuestionTableResponse>().ReverseMap();
            CreateMap<Question, QuestionUpdateRequest>().ReverseMap();

            CreateMap<Store,StoreAddRequest>().ReverseMap();
            CreateMap<Store,StoreUpdateRequest>().ReverseMap();
            CreateMap<Store,StoreTableResponse>().ReverseMap();



        }
    }
}
