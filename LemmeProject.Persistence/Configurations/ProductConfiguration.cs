using LemmeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LemmeProject.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Overview).IsRequired();
            builder.Property(b => b.HowToUse).IsRequired();
            builder.Property(b => b.Ingredients).HasMaxLength(50);
            builder.Property(b => b.EntityStatus).HasColumnName("IsDeleted");

            //Relations
            builder.HasMany(b => b.Images).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);


        }
    }
}
