using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;

namespace LemmeProject.Infrastructure.Repositories
{
    public class ApplicationErrorRepository : RepositoryBase<ApplicationError>, IApplicationErrorRepository
    {
        public ApplicationErrorRepository(LemmeAppContext context) : base(context)
        {
        }
    }
}
