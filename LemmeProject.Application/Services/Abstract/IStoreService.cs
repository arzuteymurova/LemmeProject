using LemmeProject.Application.DTOs.Questions;
using LemmeProject.Application.DTOs.Stores;
using LemmeProject.Application.Utilities.Results.Abstract;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IStoreService
    {
        Task<IResult> AddAsync(StoreAddRequest storeAddRequest);
        Task<IResult> EditAsync(StoreUpdateRequest storeUpdateRequest);
        Task<IDataResult<StoreTableResponse>> GetByIdAsync(int id);
        Task<IDataResult<List<StoreTableResponse>>> GetTableAsync();
        Task<IResult> DeleteByIdAsync(int id);
    }

}
