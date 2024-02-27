using AutoMapper;
using HotelAPI.Application.Utilities.Constants;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Helpers;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
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
            foreach (var image in productAddRequest.Images)
            {
                byte[] bytes = Convert.FromBase64String(image.FileBase64);
                image.FileName = _fileService.SavePhotoToFtp(bytes, image.FileName);
            }

            Product product = _mapper.Map<Product>(productAddRequest);
            await _productRepository.CreateAsync(product);

            return new SuccessResult(Messages.ProductAdded);

        }    

        public async Task<IResult> EditAsync(ProductUpdateRequest productUpdateRequest)
        {
            foreach (var image in productUpdateRequest.Images)
            {
                byte[] bytes = Convert.FromBase64String(image.FileBase64);
                image.FileName = _fileService.SavePhotoToFtp(bytes, image.FileName);
            }
            Product product = _mapper.Map<Product>(productUpdateRequest);
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

            return new SuccessDataResult<ProductTableResponse>(result);
        }

        public async Task<IDataResult<List<ProductTableResponse>>> GetTableAsync()
        {
            var products = await _productRepository.FindAllActiveAsync();
            var images = await _productImageRepository.FindAllActiveAsync();

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

            return new SuccessDataResult<List<ProductTableResponse>>(result);

        }

        public async Task<IDataResult<List<ProductTableResponse>>> GetProductByNameAsync(string name)
        {
            var products = await _productRepository.FindByConditionAsync(p => p.EntityStatus == EntityStatus.Active && p.Name.ToLower().Contains(name.ToLower()));
            var images = await _productImageRepository.FindAllActiveAsync();

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

            return new SuccessDataResult<List<ProductTableResponse>>(result);

        }

    }

}
