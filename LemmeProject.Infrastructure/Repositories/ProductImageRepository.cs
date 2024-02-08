using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;

namespace LemmeProject.Infrastructure.Repositories
{
    public class ProductImageRepository : RepositoryBase<Image>, IProductImageRepository
    {
        public ProductImageRepository(LemmeAppContext context) : base(context)
        {
        }
    }
}
