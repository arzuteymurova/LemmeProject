using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductService
    {
        Task<IResult> AddAsync(ProductAddRequest productAddRequest);
        Task<IResult> EditAsync(ProductUpdateRequest productUpdateRequest);
        Task<IDataResult<ProductTableResponse>> GetByIdAsync(int id);
        Task<IDataResult<List<ProductTableResponse>>> GetTableAsync();
        Task<IResult> DeleteByIdAsync(int id);
        Task<IDataResult<List<ProductTableResponse>>> GetProductByNameAsync(string name);
      
    }

}
