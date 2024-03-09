using LemmeProject.Application.DTOs.Stores;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Bearer")]

    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("AddStore")]
        public async Task<IActionResult> AddStore(StoreAddRequest storeAddRequest)
        {
            var result = await _storeService.AddAsync(storeAddRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpPost("EditStore")]
        public async Task<IActionResult> EditStore(StoreUpdateRequest storeUpdateRequest)
        {
            var result = await _storeService.EditAsync(storeUpdateRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpPost("DeleteStore/{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _storeService.DeleteByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetAllStores")]
        public async Task<IActionResult> GetStores()
        {
            var result = await _storeService.GetTableAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetStoreById/{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            var result = await _storeService.GetByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}
