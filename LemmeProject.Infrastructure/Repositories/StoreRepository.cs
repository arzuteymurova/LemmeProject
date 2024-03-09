using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;

namespace LemmeProject.Infrastructure.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(LemmeAppContext context) : base(context)
        {
        }
    }
}
