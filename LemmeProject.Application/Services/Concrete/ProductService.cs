using AutoMapper;
using HotelAPI.Application.Utilities.Constants;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Helpers;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
using LemmeProject.Domain.Entities;
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
        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository,
            IMapper mapper, IFileService fileService, IProductSearchHistoryService productSearchHistoryService)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
            _fileService = fileService;
            _productSearchHistoryService = productSearchHistoryService;
        }

        public async Task<IResult> AddAsync(ProductAddRequest productAddRequest)
        {
            Product product = _mapper.Map<Product>(productAddRequest);

            foreach (var image in productAddRequest.Images)
            {
                foreach (var productImage in product.Images)
                {
                    productImage.ImagePath = await _fileService.UploadImageAsync(image);
                }
            }

            await _productRepository.CreateAsync(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public async Task<IResult> EditAsync(ProductUpdateRequest productUpdateRequest)
        {
            Product product = _mapper.Map<Product>(productUpdateRequest);

            foreach (var image in productUpdateRequest.Images)
            {
                foreach (var productImage in product.Images)
                {
                    productImage.ImagePath = await _fileService.UploadImageAsync(image);
                }
            }

            await _productRepository.UpdateAsync(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public async Task<IResult> DeleteByIdAsync(int id)
        {
            Product product = await _productRepository.FindByIdAsync(id);
            await _productRepository.DeActivate(product);

            return new SuccessResult(Messages.ProductDeleted);
        }

        public async Task<IDataResult<ProductTableResponse>> GetByIdAsync(int id)
        {
            var products = await _productRepository.FindAllActiveAsync();
            var images = await _productImageRepository.FindAllActiveAsync();

            var result = products.Where(product => product.Id == id).Select(product => new ProductTableResponse
            {
                Id = product.Id,
                Name = product.Name,
                Overview = product.Overview,
                Ingredients = product.Ingredients,
                HowToUse = product.HowToUse,
                SkinType = product.SkinType,
                Images = images.Where(image => image.ProductId == product.Id)
                .Select(image => _fileService.GetImageAsync(image.ImagePath)).ToList()
            }).FirstOrDefault();

            await _productSearchHistoryService.AddAsync(new ProductSearchHistoryAddRequest()
            {
                SearchedDate = DateTime.Now,
                ProductId = id,

            });

            return new SuccessDataResult<ProductTableResponse>(result);
        }

        public async Task<IDataResult<List<ProductTableResponse>>> GetTableAsync()
        {
            var products = await _productRepository.FindAllActiveAsync();
            var images = await _productImageRepository.FindAllActiveAsync();

            var result = products.Select(product => new ProductTableResponse
            {
                Id = product.Id,
                Name = product.Name,
                Overview = product.Overview,
                Ingredients = product.Ingredients,
                HowToUse = product.HowToUse,
                SkinType = product.SkinType,
                Images = images.Where(image => image.ProductId == product.Id)
                .Select(image => _fileService.GetImageAsync(image.ImagePath)).ToList()
            }).ToList();

            return new SuccessDataResult<List<ProductTableResponse>>(result);
        }

        public async Task<IDataResult<List<ProductTableByNameResponse>>> GetProductByNameAsync(string name)
        {
            var products = await _productRepository.FindAllActiveAsync();

            var result = products.Where(product => product.Name.ToLower().Contains(name.ToLower())).Select(product => new ProductTableByNameResponse
            {
                Id = product.Id,
                Name = product.Name,
            }).ToList();

            return new SuccessDataResult<List<ProductTableByNameResponse>>(result);
        }



    }

}


