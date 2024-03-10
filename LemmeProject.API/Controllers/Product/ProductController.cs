using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "Bearer")]
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
            var result = await _productService.AddAsync(productAddRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditProduct(ProductUpdateRequest productUpdateRequest)
        {
            var result = await _productService.EditAsync(productUpdateRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetTableAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [AllowAnonymous]
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [AllowAnonymous]
        [HttpGet("SearchProductByName")]
        public async Task<IActionResult> SearchProductByName(string productName)
        {
            var result = await _productService.GetProductByNameAsync(productName);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



    }
}
