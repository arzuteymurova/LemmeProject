using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.SkinType
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTypeController : ControllerBase
    {
        private readonly IProductService _productService;

        public SkinTypeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("DetermineSkinType/{countsOfABC}")]
        public IActionResult DetermineSkinType(int countsOfABC)
        {
            int countOfA = countsOfABC.ToString()[0];
            int countOfB = countsOfABC.ToString()[1];
            int countOfC = countsOfABC.ToString()[2];

            if (countOfA > countOfB && countOfA > countOfC)
            {
                var type = new { Type = "Quru" };
                return Ok(type);
            }
            else if (countOfB > countOfA && countOfB > countOfC)
            {
                var type = new { Type = "Yağlı" };
                return Ok(type);
            }
            else
            {
                var type = new { Type = "Karma" };
                return Ok(type);
            }

        }

      

        [HttpGet("CheckIfSuitsSkinType/{productId}/{skinType}")]
        public async Task<IActionResult> CheckIfSuitsSkinType(int productId, string skinType)
        {
            // Retrieve product information from the service
            var productResponse = await _productService.GetByIdAsync(productId);

            // Check if the product exists
            if (!productResponse.Success || productResponse.Data == null)
            {
                return NotFound("Product not found.");
            }

            var product = productResponse.Data;
            var productSkinType = product.SkinType;

            // Ensure that the provided skin type is not null or empty
            if (string.IsNullOrEmpty(skinType))
            {
                return BadRequest("Skin type cannot be empty.");
            }

            // Check if the product suits the provided skin type
            var check = (productSkinType.Contains("bütün", StringComparison.OrdinalIgnoreCase) || productSkinType.Contains(skinType, StringComparison.OrdinalIgnoreCase));
            var result = new { Response = check };

            // Return the result
            return Ok(result);
        }

    }
}
