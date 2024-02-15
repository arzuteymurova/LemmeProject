using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.ProductStatistics
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatisticsController : ControllerBase
    {
        private readonly IProductSearchHistoryService _searchHistoryService;
        public ProductStatisticsController(IProductSearchHistoryService productSearchHistoryService)
        {
            _searchHistoryService = productSearchHistoryService;
        }

        [HttpGet("GetProductSearchHistory")]
        public async Task<IActionResult> GetProductSearchHistory()
        {
            var result = await _searchHistoryService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetProductSearchCount")]
        public async Task<IActionResult> GetProductSearchCount()
        {
            var result = await _searchHistoryService.GetSearchCountByProductAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetMostProductSearchedHours")]
        public async Task<IActionResult> GetMostProductSearchedHours()
        {
            var result = await _searchHistoryService.GetMostSearchedHoursAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetMostProductSearchedDays")]
        public async Task<IActionResult> GetMostProductSearchedDays()
        {
            var result = await _searchHistoryService.GetMostSearchedDaysAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetMostProductSearchedMonths")]
        public async Task<IActionResult> GetMostProductSearchedMonths()
        {
            var result = await _searchHistoryService.GetMostSearchedMonthsAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetSearchedProductCountDuringDayAsync")]
        public async Task<IActionResult> GetSearchedProductCountDuringDayAsync()
        {
            var result = await _searchHistoryService.GetSearchedProductCountDuringDayAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        

    }
}
