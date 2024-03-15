using LemmeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LemmeProject.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(b => b.Id);
            //builder.Property(b => b.FileName).IsRequired();
            //builder.Property(b => b.FilePath).IsRequired();
            builder.Property(b => b.EntityStatus).HasColumnName("IsDeleted");



        }
    }
}
