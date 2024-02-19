using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;

namespace LemmeProject.Infrastructure.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(LemmeAppContext context) : base(context)
        {
        }
    }
}
