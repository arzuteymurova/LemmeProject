using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.Image
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IProductImageService _imageService;

        public ImageController(IProductImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(ImageAddRequest imageAddRequest)
        {
            await _imageService.AddAsync(imageAddRequest);
            return Ok();
        }

    }
}
