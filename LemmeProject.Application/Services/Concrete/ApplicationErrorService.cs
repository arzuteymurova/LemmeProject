using LemmeProject.Application.Services.Abstract;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;

namespace LemmeProject.Application.Services.Concrete
{
    public class ApplicationErrorService : IApplicationErrorService
    {
        private readonly IApplicationErrorRepository _applicationErrorRepository;

        public ApplicationErrorService(IApplicationErrorRepository applicationErrorRepository)
        {
            _applicationErrorRepository = applicationErrorRepository;
        }

        public async Task<List<ApplicationError>> GetTableAsync()
        {
            List<ApplicationError> applicationErrors = await _applicationErrorRepository.FindAllAsync();
            return applicationErrors;
        }
    }
}
