using LemmeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LemmeProject.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Content).IsRequired();
            builder.Property(b => b.EntityStatus).HasColumnName("IsDeleted");

            //Relations


        }
    }
}
