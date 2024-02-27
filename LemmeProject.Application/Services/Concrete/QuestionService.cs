using AutoMapper;
using HotelAPI.Application.Utilities.Constants;
using LemmeProject.Application.DTOs.Questions;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;

namespace LemmeProject.Application.Services.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(QuestionAddRequest questionAddRequest)
        {
            Question question = _mapper.Map<Question>(questionAddRequest);
            await _questionRepository.CreateAsync(question);

            return new SuccessResult(Messages.QuestionAdded);

        }

        public async Task<IResult> DeleteByIdAsync(int id)
        {
            Question question = await _questionRepository.FindByIdAsync(id);
            await _questionRepository.DeActivate(question);

            return new SuccessResult(Messages.QuestionDeleted);
        }

        public async Task<IResult> EditAsync(QuestionUpdateRequest questionUpdateRequest)
        {
            Question question = _mapper.Map<Question>(questionUpdateRequest);
            await _questionRepository.UpdateAsync(question);

            return new SuccessResult(Messages.QuestionUpdated);
        }

        public async Task<IDataResult<QuestionTableResponse>> GetByIdAsync(int id)
        {
            var question = await _questionRepository.FindByIdAsync(id);
            var result = _mapper.Map<QuestionTableResponse>(question);

            return new SuccessDataResult<QuestionTableResponse>(result);

        }

        public async Task<IDataResult<List<QuestionTableResponse>>> GetTableAsync()
        {
            var questions = await _questionRepository.FindAllAsync();
            var result = _mapper.Map<List<QuestionTableResponse>>(questions);

            return new SuccessDataResult<List<QuestionTableResponse>>(result);
        }
    }
}
