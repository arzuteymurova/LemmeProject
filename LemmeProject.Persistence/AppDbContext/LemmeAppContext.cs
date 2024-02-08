using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Entities.Identity;
using LemmeProject.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LemmeProject.Persistence.AppDbContext
{
    public class LemmeAppContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public LemmeAppContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var entitiesAssembly = typeof(BaseEntity).Assembly;
            builder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        
    }
}
