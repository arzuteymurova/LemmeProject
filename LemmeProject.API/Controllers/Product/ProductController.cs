using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductAddRequest productAddRequest)
        {
            await _productService.AddAsync(productAddRequest);
            return Ok();
        }


        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products =await _productService.GetTable();
            return Ok(products);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetById(id);
            return Ok(product);
        }


        [HttpPost("EditProduct")]
        public async Task<IActionResult> EditProduct(ProductUpdateRequest productUpdateRequest)
        {
            await _productService.EditAsync(productUpdateRequest);
            return Ok();
        }

        [HttpPost("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
