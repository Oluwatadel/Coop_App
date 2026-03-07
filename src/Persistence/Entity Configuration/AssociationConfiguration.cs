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

            entity.Property(a => a.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("Id");

            entity.Property(a => a.Name)
                .HasColumnName("association_name")
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            entity.Property(a => a.Description)
                .HasColumnName("association_description")
                .HasColumnType("text")
                .IsRequired();

            entity.HasIndex(a => a.Id)
                .IsUnique();

            entity.HasIndex(a => a.Name)
                .IsUnique();
        }
    }
}
