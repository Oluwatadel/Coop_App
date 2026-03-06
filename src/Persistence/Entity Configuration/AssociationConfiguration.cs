using CoopApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoopApplication.Persistence.Entity_Configuration
{
    public class AssociationConfiguration : IEntityTypeConfiguration<Association>
    {
        public void Configure(EntityTypeBuilder<Association> entity)
        {
            entity.ToTable("association");
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Name)
                .HasColumnName("association_name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
