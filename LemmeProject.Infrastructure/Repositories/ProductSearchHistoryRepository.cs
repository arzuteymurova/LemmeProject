using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;

namespace LemmeProject.Infrastructure.Repositories
{
    public class ProductSearchHistoryRepository : RepositoryBase<ProductSearchHistory>, IProductSearchHistoryRepository
    {
        public ProductSearchHistoryRepository(LemmeAppContext context) : base(context)
        {
        }
    }
}
