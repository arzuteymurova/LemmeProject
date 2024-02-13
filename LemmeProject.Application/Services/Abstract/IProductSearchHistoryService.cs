using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Domain.Entities;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductSearchHistoryService
    {
        Task AddAsync(ProductSearchHistoryAddRequest productSearchHistoryAddRequest);
        Task<List<ProductSearchHistoryTableResponse>> GetAllAsync();
        Task<List<ProductSearchCountTableResponse>> GetSearchCountByProductAsync();
        Task<List<MostProductSearchHoursTableResponse>> GetMostSearchedHoursAsync();
        Task<List<MostProductSearchDaysTableResponse>> GetMostSearchedDaysAsync();
        Task<List<MostProductSearchMonthsTableResponse>> GetMostSearchedMonthsAsync();
        Task<List<SearchedProductCountDuringDayTableResponse>> GetSearchedProductCountDuringDayAsync();


    }
}