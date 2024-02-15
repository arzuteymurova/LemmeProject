using AutoMapper;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.ProductSearchHistory;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using System.Data;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace LemmeProject.Application.Services.Concrete
{
    public class ProductSearchHistoryService : IProductSearchHistoryService
    {
        private readonly IProductSearchHistoryRepository _productSearchHistoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductSearchHistoryService(IProductSearchHistoryRepository ProductSearchHistoryRepository, IMapper mapper, IProductRepository productRepository)
        {
            _productSearchHistoryRepository = ProductSearchHistoryRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task AddAsync(ProductSearchHistoryAddRequest ProductSearchHistoryAddRequest)
        {
            var map = _mapper.Map<ProductSearchHistory>(ProductSearchHistoryAddRequest);
            await _productSearchHistoryRepository.CreateAsync(map);
        }

        public async Task<IDataResult<List<ProductSearchHistoryTableResponse>>> GetAllAsync()
        {

            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();
            var products = await _productRepository.FindAllAsync();

            var result = from history in searchHistory
                         join product in products on history.ProductId equals product.Id
                         select new ProductSearchHistoryTableResponse()
                         {
                             ProductName = product.Name,
                             SearchedDate = history.SearchedDate,
                         };

            return new SuccessDataResult<List<ProductSearchHistoryTableResponse>>(result.ToList());
        }

        public async Task<IDataResult<List<ProductSearchCountTableResponse>>> GetSearchCountByProductAsync()
        {

            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();
            var products = await _productRepository.FindAllAsync();
            var searchCounts = from history in searchHistory
                               join product in products on history.ProductId equals product.Id
                               group product by product.Name into g
                               select new ProductSearchCountTableResponse()
                               {
                                   ProductName = g.Key,
                                   SearchCount = g.Count()
                               };

            return new SuccessDataResult<List<ProductSearchCountTableResponse>>(searchCounts.ToList());
            
        }
        public async Task<IDataResult<List<MostProductSearchHoursTableResponse>>> GetMostSearchedHoursAsync()
        {
            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();

            var searchCountsByHour = from history in searchHistory
                                     group history by history.SearchedDate.Hour into g
                                     orderby g.Count() descending
                                     select new MostProductSearchHoursTableResponse()
                                     {
                                         Hour = $"{g.Key + 4}:00",
                                         SearchCount = g.Count()
                                     };

            return new SuccessDataResult<List<MostProductSearchHoursTableResponse>>(searchCountsByHour.ToList());
        }
        public async Task<IDataResult<List<MostProductSearchDaysTableResponse>>> GetMostSearchedDaysAsync()
        {
            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();

            var searchCountsByDay = from history in searchHistory
                                    group history by history.SearchedDate.ToShortDateString() into g
                                    orderby g.Count() descending
                                    select new MostProductSearchDaysTableResponse()
                                    {
                                        Day = g.Key.ToString(),
                                        SearchCount = g.Count()
                                    };

            return new SuccessDataResult<List<MostProductSearchDaysTableResponse>>(searchCountsByDay.ToList());
        }
        public async Task<IDataResult<List<MostProductSearchMonthsTableResponse>>> GetMostSearchedMonthsAsync()
        {
            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();

            var searchCountsByMonth = from history in searchHistory
                                      group history by history.SearchedDate.Month into g
                                      orderby g.Count() descending
                                      select new MostProductSearchMonthsTableResponse()
                                      {
                                          Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                                          SearchCount = g.Count()
                                      };

            return new SuccessDataResult<List<MostProductSearchMonthsTableResponse>>(searchCountsByMonth.ToList());
        }

        public async Task<IDataResult<List<SearchedProductCountDuringDayTableResponse>>> GetSearchedProductCountDuringDayAsync()
        {
            var searchHistory = await _productSearchHistoryRepository.FindAllAsync();

            var searchCountsDuringDay = from history in searchHistory
                                        group history by history.SearchedDate.ToShortDateString() into g
                                        where g.Key == DateTime.Today.ToShortDateString()
                                        orderby g.Count() descending
                                        select new SearchedProductCountDuringDayTableResponse()
                                        {
                                            Day = g.Key.ToString(),
                                            SearchCount = g.Count()
                                        };

            return new SuccessDataResult<List<SearchedProductCountDuringDayTableResponse>>(searchCountsDuringDay.ToList());
        }

    }
}

