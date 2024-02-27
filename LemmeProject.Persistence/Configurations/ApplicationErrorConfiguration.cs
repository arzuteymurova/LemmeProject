using LemmeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LemmeProject.Persistence.Configurations
{
    public class ApplicationErrorConfiguration : IEntityTypeConfiguration<ApplicationError>
    {
        public void Configure(EntityTypeBuilder<ApplicationError> builder)
        {
            builder.ToTable("ApplicationErrors");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.ErrorMessage).IsRequired();
            builder.Property(b => b.ErrorSource).IsRequired();
            builder.Property(b => b.EntityStatus).HasColumnName("IsDeleted");

            //Relations


        }
    }
}
