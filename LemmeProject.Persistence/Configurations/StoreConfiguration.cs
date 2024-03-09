using LemmeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LemmeProject.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Adress).IsRequired();
            builder.Property(b => b.EntityStatus).HasColumnName("IsDeleted");

            //Relations
            builder.HasMany(b => b.Products).WithMany(b => b.Stores);

        }
    }
}
