using AutoMapper;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Helpers;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Enums;
using LemmeProject.Domain.Interfaces;

namespace LemmeProject.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductSearchHistoryService _productSearchHistoryService;

        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository, IMapper mapper, IFileService fileService, IProductSearchHistoryService productSearchHistoryService)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
            _fileService = fileService;
            _productSearchHistoryService = productSearchHistoryService;
        }
        public async Task AddAsync(ProductAddRequest productAddRequest)
        {
            foreach (var image in productAddRequest.Images)
            {
                byte[] bytes = Convert.FromBase64String(image.FileBase64);
                image.FileName = _fileService.SavePhotoToFtp(bytes, image.FileName);
            }
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
            foreach (var image in productUpdateRequest.Images)
            {
                byte[] bytes = Convert.FromBase64String(image.FileBase64);
                image.FileName = _fileService.SavePhotoToFtp(bytes, image.FileName);
            }
            Product product = _mapper.Map<Product>(productUpdateRequest);
            await _productRepository.UpdateAsync(product);
        }


        public async Task<ProductTableResponse> GetById(int id)
        {
            var products = await _productRepository.FindAllAsync();
            var images = await _productImageRepository.FindAllAsync();

            var result = products.Where(p => p.Id == id)
                                .GroupJoin(images,
                                           product => product.Id,
                                           image => image.ProductId,
                                           (product, imageGroup) => new ProductTableResponse
                                           {
                                               Id = product.Id,
                                               Name = product.Name,
                                               Overview = product.Overview,
                                               Ingredients = product.Ingredients,
                                               HowToUse = product.HowToUse,
                                               Images = imageGroup.Select(image => new ProductImageTableResponse
                                               {
                                                   Id = image.Id,
                                                   FileName = image.FileName,
                                                   FileBase64 = Convert.ToBase64String(_fileService.GetPhoto(image.FileName)),
                                                   ProductName = product.Name
                                               }).ToList()
                                           })
                                .FirstOrDefault();
            await _productSearchHistoryService.AddAsync(new ProductSearchHistoryAddRequest()
            {
                SearchedDate = DateTime.Now,
                ProductId = id,

            });

            return result;
        }


        public async Task<List<ProductTableResponse>> GetTable()
        {
            var products = await _productRepository.FindAllAsync();
            var images = await _productImageRepository.FindAllAsync();

            var result = products.GroupJoin(images,
                                           product => product.Id,
                                           image => image.ProductId,
                                           (product, imageGroup) => new ProductTableResponse
                                           {
                                               Id = product.Id,
                                               Name = product.Name,
                                               Overview = product.Overview,
                                               Ingredients = product.Ingredients,
                                               HowToUse = product.HowToUse,
                                               Images = imageGroup.Select(image => new ProductImageTableResponse
                                               {
                                                   Id = image.Id,
                                                   FileName = image.FileName,
                                                   FileBase64 = Convert.ToBase64String(_fileService.GetPhoto(image.FileName)),
                                                   ProductName = product.Name
                                               }).ToList()
                                           }).ToList();

            return result;
        }

        public async Task<List<ProductTableResponse>> GetProductByName(string name)
        {
            var products = await _productRepository.FindByConditionAsync(p => p.EntityStatus == EntityStatus.Active && p.Name.ToLower().Contains(name.ToLower()));
            var images = await _productImageRepository.FindAllAsync();

            var result = products.GroupJoin(images,
                                           product => product.Id,
                                           image => image.ProductId,
                                           (product, imageGroup) => new ProductTableResponse
                                           {
                                               Id = product.Id,
                                               Name = product.Name,
                                               Overview = product.Overview,
                                               Ingredients = product.Ingredients,
                                               HowToUse = product.HowToUse,
                                               Images = imageGroup.Select(image => new ProductImageTableResponse
                                               {
                                                   Id = image.Id,
                                                   FileName = image.FileName,
                                                   FileBase64 = Convert.ToBase64String(_fileService.GetPhoto(image.FileName)),
                                                   ProductName = product.Name
                                               }).ToList()
                                           }).ToList();

            return result;           

        }

        private static readonly Dictionary<string, int> searchCounts = new();
        public void LogSearch(ProductTableResponse productTableResponse)
        {
            if (searchCounts.ContainsKey(productTableResponse.Name))
            {
                searchCounts[productTableResponse.Name]++;
            }
            else
            {
                searchCounts[productTableResponse.Name] = 1;
            }

        }
        public Dictionary<string, int> GetSearchCount()
        {
            return searchCounts;
        }

    }
}
