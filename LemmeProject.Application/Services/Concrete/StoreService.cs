using AutoMapper;
using HotelAPI.Application.Utilities.Constants;
using LemmeProject.Application.DTOs.Stores;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;

namespace LemmeProject.Application.Services.Concrete
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(StoreAddRequest storeAddRequest)
        {
            var store = _mapper.Map<Store>(storeAddRequest);
            await _storeRepository.CreateAsync(store);

            return new SuccessResult(Messages.StoreAdded);
        }

        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var store = await _storeRepository.FindByIdAsync(id);
            await _storeRepository.DeActivate(store);

            return new SuccessResult(Messages.StoreDeleted);
        }

        public async Task<IResult> EditAsync(StoreUpdateRequest storeUpdateRequest)
        {
            var store = _mapper.Map<Store>(storeUpdateRequest);
            await _storeRepository.UpdateAsync(store);

            return new SuccessResult(Messages.StoreUpdated);
        }

        public async Task<IDataResult<StoreTableResponse>> GetByIdAsync(int id)
        {
            var store = await _storeRepository.FindByIdAsync(id);
            var result = _mapper.Map<StoreTableResponse>(store);

            return new SuccessDataResult<StoreTableResponse>(result);
        }

        public async Task<IDataResult<List<StoreTableResponse>>> GetTableAsync()
        {
            var stores = await _storeRepository.FindAllActiveAsync();
            var result = _mapper.Map<List<StoreTableResponse>>(stores);

            return new SuccessDataResult<List<StoreTableResponse>>(result);
        }
    }
}
