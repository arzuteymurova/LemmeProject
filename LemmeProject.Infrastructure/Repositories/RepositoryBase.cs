using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Enums;
using LemmeProject.Domain.Interfaces;
using LemmeProject.Persistence.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LemmeProject.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly LemmeAppContext repositoryContextBase;

        public RepositoryBase(LemmeAppContext context)
        {
            repositoryContextBase = context;
        }


        #region Asynchronous methods
        public async Task<TEntity> FindByIdAsync(int Id)
        {
            TEntity entity;
            entity = await repositoryContextBase.Set<TEntity>().FindAsync(Id);
            return entity;
        }
        public async Task<List<TEntity>> FindAllActiveAsync()
        {
            List<TEntity> entities;
            entities = await repositoryContextBase.Set<TEntity>().Where(x => x.EntityStatus == EntityStatus.Active).ToListAsync();
            return entities;
        }
        public async Task<List<TEntity>> FindAllAsync()
        {
            List<TEntity> entities;
            entities = await repositoryContextBase.Set<TEntity>().ToListAsync();
            return entities;
        }
        public async Task<List<TEntity>> FindAllActiveAsNoTrackingAsync()
        {
            IQueryable<TEntity> entities;
            entities = repositoryContextBase.Set<TEntity>().Where(x => x.EntityStatus == EntityStatus.Active).AsNoTracking();
            return await entities.ToListAsync();
        }
        public async Task<List<TEntity>> FindAllAsNoTrackingAsync()
        {
            IQueryable<TEntity> entities;
            entities = repositoryContextBase.Set<TEntity>().AsNoTracking();
            return await entities.ToListAsync();
        }
        public async Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            List<TEntity> entities;
            entities = await repositoryContextBase.Set<TEntity>().Where(expression).ToListAsync();
            return entities;
        }
        public async Task<TEntity> FindByConditionFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            TEntity entitiy;
            entitiy = await repositoryContextBase.Set<TEntity>().Where(expression).FirstOrDefaultAsync();
            return entitiy;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await repositoryContextBase.Set<TEntity>().AddAsync(entity);
            await repositoryContextBase.SaveChangesAsync();
        }
        public async Task CreateRangeAsync(List<TEntity> entites)
        {
            await repositoryContextBase.Set<TEntity>().AddRangeAsync(entites);
            await repositoryContextBase.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            repositoryContextBase.Set<TEntity>().Update(entity);
            await repositoryContextBase.SaveChangesAsync();
        }
        public async Task DeActivate(TEntity entity)
        {
            entity.EntityStatus = EntityStatus.InActive;
            repositoryContextBase.Set<TEntity>().Update(entity);
            await repositoryContextBase.SaveChangesAsync();
        }
        #endregion
    }
}
