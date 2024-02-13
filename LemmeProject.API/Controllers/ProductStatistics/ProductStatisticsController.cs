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
            var data = await _searchHistoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetProductSearchCount")]
        public async Task<IActionResult> GetProductSearchCount()
        {
            var data = await _searchHistoryService.GetSearchCountByProductAsync();
            return Ok(data);
        }


        [HttpGet("GetMostProductSearchedHours")]
        public async Task<IActionResult> GetMostProductSearchedHours()
        {
            var data = await _searchHistoryService.GetMostSearchedHoursAsync();
            return Ok(data);
        }

        [HttpGet("GetMostProductSearchedDays")]
        public async Task<IActionResult> GetMostProductSearchedDays()
        {
            var data = await _searchHistoryService.GetMostSearchedDaysAsync();
            return Ok(data);
        }

        [HttpGet("GetMostProductSearchedMonths")]
        public async Task<IActionResult> GetMostProductSearchedMonths()
        {
            var data = await _searchHistoryService.GetMostSearchedMonthsAsync();
            return Ok(data);
        }

        [HttpGet("GetSearchedProductCountDuringDayAsync")]
        public async Task<IActionResult> GetSearchedProductCountDuringDayAsync()
        {
            var data = await _searchHistoryService.GetSearchedProductCountDuringDayAsync();
            return Ok(data);
        }
        

    }
}
