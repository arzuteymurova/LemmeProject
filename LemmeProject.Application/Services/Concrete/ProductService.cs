using AutoMapper;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;

namespace LemmeProject.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(ProductAddRequest productAddRequest)
        {
            Product product = _mapper.Map<Product>(productAddRequest);
            await _productRepository.CreateAsync(product);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Product product = await _productRepository.FindByIdAsync(id);
            await _productRepository.DeActivate(product);
        }

        public async Task EditAsync(ProductUpdateRequest productUpdateRequest)
        {
            Product product = _mapper.Map<Product>(productUpdateRequest);
            await _productRepository.UpdateAsync(product);
        }

        public async Task<ProductTableResponse> GetById(int id)
        {
            Product product = await _productRepository.FindByIdAsync(id);
            ProductTableResponse productTable = _mapper.Map<ProductTableResponse>(product);

            return productTable;
        }

        public async Task<List<ProductTableResponse>> GetTable()
        {
            List<Product> products = await _productRepository.FindAllAsync();
            List<ProductTableResponse> productTable = _mapper.Map<List<ProductTableResponse>>(products);

            return productTable;
        }
    }
}
