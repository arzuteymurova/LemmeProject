using LemmeProject.Application.DTOs.Products;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductService
    {
        Task AddAsync(ProductAddRequest productAddRequest);
        Task EditAsync(ProductUpdateRequest productUpdateRequest);
        Task<ProductTableResponse> GetById(int id);
        Task<List<ProductTableResponse>> GetTable();
        Task DeleteByIdAsync(int id);
    }
    
}
