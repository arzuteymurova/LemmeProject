using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Domain.Entities;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IProductSearchHistoryService
    {
        Task AddAsync(ProductSearchHistoryAddRequest productSearchHistoryAddRequest);
        Task<IDataResult<List<ProductSearchHistoryTableResponse>>> GetAllAsync();
        Task<IDataResult<List<ProductSearchCountTableResponse>>> GetSearchCountByProductAsync();
        Task<IDataResult<List<MostProductSearchHoursTableResponse>>> GetMostSearchedHoursAsync();
        Task<IDataResult<List<MostProductSearchDaysTableResponse>>> GetMostSearchedDaysAsync();
        Task<IDataResult<List<MostProductSearchMonthsTableResponse>>> GetMostSearchedMonthsAsync();
        Task<IDataResult<List<SearchedProductCountDuringDayTableResponse>>> GetSearchedProductCountDuringDayAsync();


    }
}