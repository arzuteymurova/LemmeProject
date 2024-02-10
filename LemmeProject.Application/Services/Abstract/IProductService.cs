using LemmeProject.Application.DTOs.Products;
using LemmeProject.Domain.Entities;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductService
    {
        Task AddAsync(ProductAddRequest productAddRequest);
        Task EditAsync(ProductUpdateRequest productUpdateRequest);
        Task<ProductTableResponse> GetById(int id);
        Task<List<ProductTableResponse>> GetTable();
        Task DeleteByIdAsync(int id);
        Task<List<ProductTableResponse>> GetProductByName(string name);
        void LogSearch(ProductTableResponse productTableResponse);
        Dictionary<string, int> GetSearchCount();
    }
    
}
