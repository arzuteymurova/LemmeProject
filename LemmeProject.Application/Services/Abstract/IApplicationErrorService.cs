using LemmeProject.Domain.Entities;

namespace LemmeProject.Application.Services.Abstract
{
    public interface IApplicationErrorService
    {
        Task<List<ApplicationError>> GetTableAsync();
    }
}
