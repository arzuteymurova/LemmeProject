using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductImageService
    {
        Task AddAsync(ImageAddRequest imageAddRequest);
       
    }
}
