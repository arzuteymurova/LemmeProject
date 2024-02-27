using LemmeProject.Application.DTOs.Products;
using LemmeProject.Application.DTOs.Questions;
using LemmeProject.Application.Utilities.Results.Abstract;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IQuestionService
    {
        Task<IResult> AddAsync(QuestionAddRequest questionAddRequest);
        Task<IResult> EditAsync(QuestionUpdateRequest questionUpdateRequest);
        Task<IDataResult<QuestionTableResponse>> GetByIdAsync(int id);
        Task<IDataResult<List<QuestionTableResponse>>> GetTableAsync();
        Task<IResult> DeleteByIdAsync(int id);

    }

}
